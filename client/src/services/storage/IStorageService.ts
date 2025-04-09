export interface IStorageService {
  uploadFile(file: File): Promise<string>;
}
