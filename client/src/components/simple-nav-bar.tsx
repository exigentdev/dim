import { Menu, PlusCircle } from 'lucide-react';
import logoSvg from '../assets/logo.svg';
import { ModeToggle } from './mode-toggle';
import { Avatar, AvatarFallback, AvatarImage } from './ui/avatar';
import { Button } from './ui/button';
import { CreatePostModal } from './create-post-modal';
import { useState } from 'react';
import Avvvatars from 'avvvatars-react';

import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from './ui/sheet';
import { decodeJWT } from '@/utils';

interface SimpleNavBarProps {
  isLoggedIn?: boolean;
  onLogout?: () => void;
  avatarUrl?: string;
}

export const SimpleNavBar = (props: SimpleNavBarProps) => {
  const [createPostModalOpen, setCreatePostModalOpen] =
    useState<boolean>(false);

  const { given_name } = props.isLoggedIn ? decodeJWT() : {};

  const onCreatePostButtonClick = () => {
    setCreatePostModalOpen(true);
  };

  return (
    <section className="bg-background/95 supports-[backdrop-filter]:bg-background/60 sticky top-0 z-50 w-full border-b backdrop-blur">
      <div className="py-4">
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
          <div className="hidden items-center gap-4 pr-4 md:flex">
            {props.isLoggedIn && (
              <>
                <Button variant={'ghost'} onClick={onCreatePostButtonClick}>
                  <PlusCircle className="h-4 w-4" />
                  Create Post
                </Button>
                <Avvvatars value={given_name!} />
                <Button variant={'outline'} onClick={props.onLogout}>
                  Logout
                </Button>
              </>
            )}

            <ModeToggle />
          </div>
          <div className="flex pr-4 md:hidden">
            {props.isLoggedIn && (
              <Sheet>
                <SheetTrigger asChild>
                  <Button variant="outline" size="icon">
                    <Menu className="size-4" />
                  </Button>
                </SheetTrigger>
                <SheetContent className="w-[300px] overflow-y-auto md:w-[540px]">
                  <SheetHeader className="flex flex-row justify-between">
                    <SheetTitle className="hidden">Sidebar</SheetTitle>
                    <SheetDescription className="hidden">
                      Sidebar
                    </SheetDescription>
                    <div className="flex items-center gap-6">
                      <a href={''} className="flex items-center gap-2">
                        <img
                          src={logoSvg}
                          className="w-10 rounded-full dark:bg-white"
                          alt={'alt'}
                        />
                        <span className="text-lg font-semibold">
                          {"Dogs I've Met"}
                        </span>
                      </a>
                    </div>
                  </SheetHeader>
                  <div className="flex flex-1 flex-col gap-4">
                    <div className="flex items-center justify-between p-4">
                      <div className="flex items-center gap-2">
                        <Avvvatars value={given_name!} />
                        {given_name}
                      </div>
                      <ModeToggle />
                    </div>
                    <div className="flex">
                      <Button
                        className="mx-4 flex-1"
                        variant={'outline'}
                        onClick={onCreatePostButtonClick}
                      >
                        <PlusCircle className="h-4 w-4" />
                        Create Post
                      </Button>
                    </div>
                  </div>
                  <div className="mx-4 my-4 flex gap-4">
                    <Button
                      className="flex-1"
                      variant={'outline'}
                      onClick={props.onLogout}
                    >
                      Logout
                    </Button>
                  </div>
                </SheetContent>
              </Sheet>
            )}
          </div>
        </nav>
      </div>
      <CreatePostModal
        open={createPostModalOpen}
        onOpenChange={setCreatePostModalOpen}
      />
    </section>
  );
};
