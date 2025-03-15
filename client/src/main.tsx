import { StrictMode } from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import { App } from './app';
import { ThemeProvider } from './components/themeProvider';

const rootElement = document.getElementById('root')!;
if (!rootElement.innerHTML) {
  const root = ReactDOM.createRoot(rootElement);
  root.render(
    <StrictMode>
      <ThemeProvider defaultTheme="dark" storageKey="dim-ui-theme">
        <App />
      </ThemeProvider>
    </StrictMode>,
  );
}
