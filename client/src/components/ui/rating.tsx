import clsx from 'clsx';
import { StarHalfIcon, StarIcon } from 'lucide-react';

/**
 * Shows a rating with stars.
 */
export const Rating = ({
  className,
  rating = 5,
  maxRating = 5,
  size = 'large',
  onClick,
}: {
  className?: string;
  rating?: number;
  maxRating?: number;
  size?: 'small' | 'medium' | 'large';
  onClick?: (index: number) => void;
}) => {
  return (
    <div
      className={clsx('flex items-center gap-1', className)}
      aria-description={`Rating: ${rating} out of ${maxRating}`}
    >
      {Array.from({ length: maxRating }).map((_, index) => {
        return (
          <div
            key={index}
            className={clsx(
              size === 'small' ? 'h-3 w-3' : '',
              size === 'medium' ? 'h-4 w-4' : '',
              size === 'large' ? 'h-5 w-5' : '',
            )}
            onClick={() => {
              onClick && onClick(index);
            }}
          >
            {
              // Return half star for half ratings
              rating % 1 !== 0 &&
              index === Math.floor(rating) &&
              index + 1 === Math.ceil(rating) ? (
                <div className="relative" key={index}>
                  <StarIcon
                    className="absolute top-0 left-0 h-full w-full fill-gray-100 text-gray-100"
                    aria-hidden="true"
                  />

                  <StarHalfIcon
                    className="relative z-10 h-full w-full fill-yellow-500 text-yellow-500"
                    aria-hidden="true"
                  />
                </div>
              ) : (
                <StarIcon
                  key={index}
                  className={clsx('h-full w-full', {
                    'fill-yellow-500 text-yellow-500': index < rating,
                    'fill-gray-100 text-gray-100': index >= rating,
                  })}
                  aria-hidden="true"
                />
              )
            }
          </div>
        );
      })}
    </div>
  );
};
