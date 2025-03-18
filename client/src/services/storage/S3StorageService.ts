import {
  S3Client,
  PutObjectCommand,
  PutObjectRequest,
} from '@aws-sdk/client-s3';
import { IStorageService } from './IStorageService';

export function createS3StorageService(): IStorageService {
  const bucketName = import.meta.env.VITE_REACT_APP_S3_BUCKET_NAME!;
  const region = import.meta.env.VITE_REACT_APP_S3_REGION!;
  const accessKeyId = import.meta.env.VITE_REACT_APP_S3_ACCESS_KEY!;
  const secretAccessKey = import.meta.env.VITE_REACT_APP_S3_SECRET_KEY!;

  if (!bucketName || !region || !accessKeyId || !secretAccessKey) {
    throw new Error('AWS credentials are missing from environment variables.');
  }

  const client = new S3Client({
    region,
    credentials: {
      accessKeyId,
      secretAccessKey,
    },
    requestChecksumCalculation: 'WHEN_REQUIRED',
  });

  const uploadFile = async (file: File, username: string): Promise<string> => {
    if (!file) throw new Error('File is undefined');

    const uuid = crypto.randomUUID();
    const key = `postimage/${username}/${uuid}`;

    const params: PutObjectRequest = {
      Bucket: bucketName,
      Key: key,
      Body: file,
      ContentType: file.type,
    };

    try {
      const command = new PutObjectCommand(params);
      await client.send(command);

      return `https://${bucketName}.s3.${region}.amazonaws.com/${key}`;
    } catch (error) {
      console.error('S3 upload error:', error);
      throw new Error('Failed to upload image.');
    }
  };

  return {
    uploadFile,
  };
}
