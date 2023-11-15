import * as React from 'react';
import { FC, useState } from 'react';
import { useRestAPI } from '../../hooks/useRestAPI';
import { IMainComponentProps } from './IMainComponentProps';
import LoginComponent from '../Login/LoginComponent'
import styles from './MainComponent.module.scss'
import ForecastsComponent from '../Forecasts/ForecastsComponent';

const MainComponent: FC<IMainComponentProps> = (props) => {
    const [token, setToken] = useState("");
    const [isLogIn, setIsLogIn] = useState(false);
    

    return (
        <div className={styles.MainComponent}>
            {isLogIn === false && (
                <LoginComponent setToken={setToken} setIsLogIn={setIsLogIn} />
            )}
            {isLogIn === true && (
                < ForecastsComponent token={token} />
            )}
        </div>
    );
};

export default MainComponent;