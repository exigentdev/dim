import logoSvg from '../../assets/logo.svg';
import { ModeToggle } from '../mode-toggle';
import { Avatar, AvatarFallback, AvatarImage } from './avatar';
import { Button } from './button';

interface SimpleNavBarProps {
  isLoggedIn?: boolean;
  onLogout?: () => void;
  avatarUrl?: string;
}

export const SimpleNavBar = (props: SimpleNavBarProps) => {
  return (
    <section className="fixed top-0 w-full py-4">
      <div>
        <nav className="flex justify-between pl-4">
          <div className="flex items-center gap-6">
            <a href={''} className="flex items-center gap-2">
              <img
                src={logoSvg}
                className="w-10 rounded-full dark:bg-white"
                alt={'alt'}
              />
              <span className="text-lg font-semibold">{"Dogs I've Met"}</span>
            </a>
          </div>
          <div className="mx-4 flex gap-4">
            {props.isLoggedIn ? (
              <>
                <Avatar>
                  <AvatarImage
                    src="https://github.com/shadcn.png"
                    alt="@shadcn"
                  />
                  <AvatarFallback>CN</AvatarFallback>
                </Avatar>
                <Button variant={'outline'} onClick={props.onLogout}>
                  Logout
                </Button>
              </>
            ) : (
              <></>
            )}

            <ModeToggle />
          </div>
        </nav>
      </div>
      <div className="my-2 h-[2px] w-full bg-gray-200 dark:bg-gray-700"></div>
    </section>
  );
};
