/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { AppUserDto } from "./app-user-dto";
import { DogDto } from "./dog-dto";
import { LikedPostDto } from "./liked-post-dto";

export interface PostDto {
    id: number;
    dateCreated: Date;
    appUser: AppUserDto;
    dog: DogDto;
    likedByUsers: LikedPostDto[];
}
