/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { DogImageDto } from "./dog-image-dto";

export interface DogDto {
    id: number;
    dateMet: Date;
    rating: number;
    comment: string;
    breed: string;
    name: string;
    dogImages: DogImageDto[];
}
