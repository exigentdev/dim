import { Avatar, AvatarFallback, AvatarImage } from './ui/avatar';
import { Dialog, DialogContent, DialogHeader, DialogTitle } from './ui/dialog';
import { Input } from './ui/input';
import { Textarea } from './ui/textarea';

type CreatePostModalProps = {
  open: boolean;
  onOpenChange: (open: boolean) => void;
};

export function CreatePostModal(props: CreatePostModalProps) {
  return (
    <Dialog open={props.open} onOpenChange={props.onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Create Post</DialogTitle>
        </DialogHeader>
        <div className="flex items-start gap-3 py-4">
          <Avatar>
            <AvatarImage src="https://github.com/shadcn.png" alt="@shadcn" />
            <AvatarFallback>CN</AvatarFallback>
          </Avatar>
          <Textarea
            className="min-h-[100px] resize-none border-none p-0 shadow-none focus-visible:ring-0"
            placeholder="Tell us about the bestest doggo!"
          />
        </div>
      </DialogContent>
    </Dialog>
  );
}
