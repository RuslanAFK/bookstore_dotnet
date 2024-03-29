import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App.tsx';
import {Provider} from "react-redux";
import 'bootstrap/dist/css/bootstrap.css';
import {store} from "./features/shared/store/store.ts";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <Provider store={store}>
        <App/>
    </Provider>
);
