import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.module.css';
import reportWebVitals from './reportWebVitals';
import MainComponent from './components/Main/MainComponent';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <React.StrictMode>
        <MainComponent test = "test" />
  </React.StrictMode>
);


// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
