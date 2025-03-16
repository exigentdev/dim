import { ImageIcon, X } from 'lucide-react';
import { Avatar, AvatarFallback, AvatarImage } from './ui/avatar';
import { Button } from './ui/button';
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from './ui/dialog';
import { Textarea } from './ui/textarea';
import { Input } from './ui/input';
import { useRef, useState } from 'react';
import {
  S3Client,
  PutObjectCommand,
  PutObjectRequest,
} from '@aws-sdk/client-s3';
import { decodeJWT } from '@/utils';
import { v4 as uuidv4 } from 'uuid';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createPost } from '@/api/posts';
import { CreatePostDto } from 'types/create-post-dto';

type CreatePostModalProps = {
  open: boolean;
  onOpenChange: (open: boolean) => void;
};

export function CreatePostModal(props: CreatePostModalProps) {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [imagePreview, setImagePreview] = useState<string | ArrayBuffer>();
  const [file, setFile] = useState<File>();
  const queryClient = useQueryClient();

  const postMutation = useMutation({
    mutationKey: ['createPost'],
    mutationFn: createPost,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['posts'] });
    },
    onError: (error) => {
      console.error(error);
    },
  });

  const handleSubmit = async () => {
    if (!file) {
      throw Error('File is undefined');
    }

    // TODO: move these to .env
    const bucketName = 'dim-image-bucket';
    const region = 'us-east-2';
    const accessKeyId = '';
    const secretAccessKey = '';

    const { given_name: username } = decodeJWT();

    const client = new S3Client({
      region,
      credentials: {
        accessKeyId,
        secretAccessKey,
      },
      requestChecksumCalculation: 'WHEN_REQUIRED',
    });

    const uuid = uuidv4();

    const params: PutObjectRequest = {
      Bucket: bucketName,
      Key: `postimage/${username}/${uuid}`,
      Body: file,
      ContentType: file.type,
    };

    try {
      const command = new PutObjectCommand(params);
      await client.send(command);
    } catch (error) {
      throw Error('failed to upload image');
    }

    const url = `https://${bucketName}.s3.${region}.amazonaws.com/postimage/${username}/${uuid}`;

    const createPostDto: CreatePostDto = {
      dogImageUrls: [url],
      dateMet: new Date(),
      breed: 'test',
      comment: 'test',
      name: 'test',
      rating: 4,
    };

    postMutation.mutate(createPostDto);
  };

  const onImageInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    event.preventDefault();

    if (!event.target.files) {
      return;
    }

    const file = event.target.files[0];
    setFile(file);

    const reader = new FileReader();

    reader.onload = function (event) {
      if (!event.target?.result) {
        return;
      }
      setImagePreview(event.target.result);
    };

    reader.readAsDataURL(file);
  };

  const handleRemoveImage = () => {
    setImagePreview(undefined);
    setFile(undefined);
  };

  return (
    <Dialog open={props.open} onOpenChange={props.onOpenChange}>
      <DialogContent aria-describedby="">
        <DialogHeader>
          <DialogTitle>Create Post</DialogTitle>
          <DialogDescription>Share your favorite dogs!</DialogDescription>
        </DialogHeader>
        <div className="flex flex-col gap-3 py-4">
          <div className="flex gap-3">
            <div className="my-2">
              <Avatar>
                <AvatarImage
                  src="https://github.com/shadcn.png"
                  alt="@shadcn"
                />
                <AvatarFallback>CN</AvatarFallback>
              </Avatar>
            </div>
            <Textarea
              className="min-h-[100px] resize-none border-none p-4 shadow-none focus-visible:ring-0"
              placeholder="Tell us about the bestest doggo!"
              name="comment"
            />
          </div>
          {imagePreview && (
            <div className="mt-3 flex justify-center overflow-hidden rounded-md">
              <div className="relative">
                <Button
                  variant="destructive"
                  size="icon"
                  className="absolute top-1 right-1 h-8 w-8 rounded-full opacity-90"
                  onClick={handleRemoveImage}
                >
                  <X className="h-4 w-4" />
                </Button>
                <img
                  src={imagePreview as string}
                  alt="Post preview"
                  className="h-auto max-h-[150px] object-contain"
                />
              </div>
            </div>
          )}
        </div>
        <DialogFooter className="flex flex-row items-center justify-between sm:justify-between">
          <Button
            variant="outline"
            size="sm"
            type="button"
            onClick={() => fileInputRef.current?.click()}
          >
            <ImageIcon className="mr-2 h-4 w-4" />
            Add Image
          </Button>
          <Input
            id="file-input"
            className="hidden"
            type="file"
            accept="image/png, image/jpeg"
            onChange={onImageInputChange}
            ref={fileInputRef}
          />
          <Button type="submit" onClick={handleSubmit}>
            Post
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
