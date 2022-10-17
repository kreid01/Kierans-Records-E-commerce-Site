import { React } from 'react';
import ReactDOM from 'react-dom/client';
import './styles.css';
import App from './App';
import {BrowserRouter, unstable_HistoryRouter } from 'react-router-dom';
import { ThemeProvider } from './components/ThemeContext';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <>
    <ThemeProvider>
      <BrowserRouter history={unstable_HistoryRouter }>
        <App />
      </BrowserRouter>
    </ThemeProvider>
  </>
)
