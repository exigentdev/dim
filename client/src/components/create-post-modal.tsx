import { ImageIcon, X } from 'lucide-react';
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
import { decodeJWT } from '@/utils';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createPost } from '@/api/posts';
import { CreatePostDto } from 'types/create-post-dto';
import { z } from 'zod';
import { toast } from 'sonner';
import { useStorage } from '@/context/storage/StorageContext';
import { Rating } from './ui/rating';
import { Label } from './ui/label';

const createPostSchema = z.object({
  comment: z.string().min(1),
  breed: z.string(),
  name: z.string(),
});

type CreatePostModalProps = {
  open: boolean;
  onOpenChange: (open: boolean) => void;
};

export function CreatePostModal(props: CreatePostModalProps) {
  const fileInputRef = useRef<HTMLInputElement>(null);
  const [imagePreview, setImagePreview] = useState<string | ArrayBuffer>();
  const [file, setFile] = useState<File>();
  const queryClient = useQueryClient();
  const storageService = useStorage();
  const [ratingNum, setRatingNum] = useState(5);

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

  const onSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!file) {
      toast.error('No image selected. Please choose an image.');

      return;
    }

    const formData = new FormData(event.currentTarget);
    const createPostInfo = Object.fromEntries(formData.entries());
    const createPostInfoResult = createPostSchema.safeParse(createPostInfo);

    if (!createPostInfoResult.success) {
      createPostInfoResult.error.errors.forEach((error) => {
        toast.error(error.message);
      });

      return;
    }

    const { given_name: username } = decodeJWT();

    const url = await storageService.uploadFile(file, username);

    const createPostDto: CreatePostDto = {
      dogImageUrls: [url],
      dateMet: new Date(),
      comment: createPostInfoResult.data.comment,
      breed: createPostInfoResult.data.breed,
      name: createPostInfoResult.data.name,
      rating: ratingNum,
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

  const onStarClick = (index: number) => {
    setRatingNum(index + 1);
  };

  return (
    <Dialog open={props.open} onOpenChange={props.onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Create Post</DialogTitle>
          <DialogDescription>Share your favorite dogs!</DialogDescription>
        </DialogHeader>
        <form id="create-form" onSubmit={onSubmit}>
          <div className="grid gap-3 py-4">
            <div className="grid gap-3">
              <div>
                <Textarea
                  className="min-h-[100px] resize-none border-none p-4 shadow-none focus-visible:ring-0"
                  placeholder="Tell us about the bestest doggo!"
                  name="comment"
                />
              </div>
              <div className="grid w-full gap-1.5">
                <Label htmlFor="name">Name</Label>
                <Input
                  className="border-none shadow-none focus-visible:ring-0"
                  id="name"
                  placeholder="LeBark James"
                  name="name"
                />
              </div>
              <div className="grid w-full gap-1.5">
                <Label htmlFor="breed">Breed</Label>
                <Input
                  className="border-none shadow-none focus-visible:ring-0"
                  id="breed"
                  placeholder="Poodle"
                  name="breed"
                />
              </div>
              <div className="grid grid-cols-[auto_1fr] items-center gap-3">
                <Label className="text-base" htmlFor="rating">
                  Rating:
                </Label>
                <Rating rating={ratingNum} onClick={onStarClick} />
              </div>
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
        </form>
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
          <Button type="submit" form="create-form">
            Post
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}
