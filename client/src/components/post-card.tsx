import { Card, CardContent, CardFooter } from '@/components/ui/card';
import { Rating } from '@/components/ui/rating';
import Avvvatars from 'avvvatars-react';
import { PostDto } from 'types/post-dto';
import { Button } from './ui/button';
import { Heart } from 'lucide-react';
import { SlidingNumber } from './animate-ui/sliding-number';

interface PostCardProps {
  post: PostDto;
}

export const PostCard = (props: PostCardProps) => {
  const { post } = props;

  const onLikeClick = () => {};

  return (
    <Card key={post.id}>
      <CardContent className="flex flex-col gap-y-3">
        <img
          alt="dog image"
          src={post.dog.dogImages[0].imageUrl}
          className="mx-auto"
        />
        <div className="flex items-center gap-2">
          <Avvvatars value={post.appUser.userName} />
          {post.appUser.userName}
        </div>
        <div>
          <div className="flex justify-between">
            <p className="text-xl">{post.dog.name}</p>
            <div className="grid grid-cols-[auto_1fr] items-center gap-3">
              <Rating rating={post.dog.rating} />
            </div>
          </div>
          <p className="text-sm opacity-60">{post.dog.breed}</p>
          <p className="text-sm opacity-60">
            Date Met:{' '}
            {new Date(post.dog.dateMet).toLocaleDateString('en-US', {
              month: 'short',
              day: '2-digit',
              year: 'numeric',
            })}
          </p>
        </div>
      </CardContent>
      <CardFooter className="justify-end">
        <Button onClick={onLikeClick}>
          <Heart fill="#000000" />
          Like
          <SlidingNumber number={post.likedByUsers.length} />
        </Button>
      </CardFooter>
    </Card>
  );
};
