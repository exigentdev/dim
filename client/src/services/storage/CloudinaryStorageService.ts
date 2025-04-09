import { IStorageService } from './IStorageService';

export function createCloudinaryStorageService(): IStorageService {
  const uploadFile = async (file: File): Promise<string> => {
    if (!file) throw new Error('File is undefined');

    const fd = new FormData();
    fd.append('file', file);
    fd.append('upload_preset', 'dim_post_images');
    fd.append('folder', `posts`);

    try {
      const response = await fetch(
        'https://api.cloudinary.com/v1_1/dimimages/image/upload',
        {
          method: 'POST',
          body: fd,
        },
      );

      const data = await response.json();

      return data.secure_url;
    } catch (error) {
      console.error('Cloudinary upload error:', error);
      throw new Error('Failed to upload image.');
    }
  };

  return {
    uploadFile,
  };
}
