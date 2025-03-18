export interface IStorageService {
  uploadFile(file: File, username: string): Promise<string>;
}
