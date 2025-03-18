import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { App } from './app';
import { ThemeProvider } from './components/themeProvider';
import { StorageProvider } from './context/storage/StorageContext';

const rootElement = document.getElementById('root')!;
if (!rootElement.innerHTML) {
  const root = ReactDOM.createRoot(rootElement);
  root.render(
    <StrictMode>
      <ThemeProvider defaultTheme="dark" storageKey="dim-ui-theme">
        <StorageProvider>
          <App />
        </StorageProvider>
      </ThemeProvider>
    </StrictMode>,
  );
}
