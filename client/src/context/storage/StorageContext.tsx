import { IStorageService } from '@/services/storage/IStorageService';
import { createCloudinaryStorageService } from '@/services/storage/CloudinaryStorageService';
import { createContext, useContext } from 'react';

const StorageContext = createContext<IStorageService | null>(null);

type StorageProviderProps = {
  children: React.ReactNode;
  storageService?: IStorageService;
};

export const StorageProvider = ({
  children,
  storageService = createCloudinaryStorageService(),
}: StorageProviderProps) => {
  return (
    <StorageContext.Provider value={storageService}>
      {children}
    </StorageContext.Provider>
  );
};

export const useStorage = () => {
  const context = useContext(StorageContext);

  if (!context) {
    throw new Error('useStorage must be used within a StorageProvider');
  }

  return context;
};
