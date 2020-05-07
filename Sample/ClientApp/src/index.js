import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
// import registerServiceWorker from './registerServiceWorker';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

window.addEventListener('error', function ({ error }) {
  console.log(error.stack);

    fetch('/api/retrace', {
        method: 'POST',
        body: error.stack,
    })
        .then((res) => res.text())
        .then(retraced => {
            console.log(retraced);
        }).catch(err => {
        console.error(err);
    })
});
window.addEventListener('unhandledrejection', function (rejection) {
    console.log(rejection.reason.stack);

    fetch('/api/retrace', {
        method: 'POST',
        body: rejection.reason.stack,
    })
        .then((res) => res.text())
        .then(retraced => {
            console.log(retraced);
        }).catch(err => {
            console.error(err);
    })
})

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
    <App />
  </BrowserRouter>,
  rootElement);

throw new Error('YIHAA');

// registerServiceWorker();

