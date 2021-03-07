import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import * as serviceWorker from './serviceWorker';
import { Router, BrowserRouter } from 'react-router-dom';
import HttpsRedirect from 'react-https-redirect'; //need to use it?

import App from './App';

ReactDOM.render(
    <BrowserRouter>
      <App />
    </BrowserRouter>
  ,
  document.getElementById('root')
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
