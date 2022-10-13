import { React } from 'react';
import ReactDOM from 'react-dom/client';
import './styles.css';
import App from './App';
import { BrowserRouter } from 'react-router-dom';
import { ThemeProvider } from './components/ThemeContext';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <>
    <ThemeProvider>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </ThemeProvider>
  </>
)
