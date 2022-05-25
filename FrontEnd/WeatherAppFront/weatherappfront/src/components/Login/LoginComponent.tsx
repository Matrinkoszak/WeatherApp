import * as React from 'react';
import { FC, useState } from 'react';
import { useRestAPI } from '../../hooks/useRestAPI';
import { ILoginComponentProps } from './ILoginComponentProps';
import styles from './LoginComponent.module.scss'

const LoginComponent: FC<ILoginComponentProps> = (props) => {
    const { getToken } = useRestAPI();
    const [token, setToken] = useState("");
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");

    const handleSumbit = (event: React.FormEvent<HTMLFormElement>): void => {
        event.preventDefault();
        getToken(login, password).then((response) => { setToken(response) });
    }

    const handleLoginChange = (event: React.ChangeEvent<HTMLInputElement>): void => {
        event.preventDefault();
        setLogin(event.target.value);
    }

    const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>): void => {
        event.preventDefault();
        setPassword(event.target.value);
    }

    return (
        <div className={styles.LoginComponent}>
            {token}
            <form className={styles.Form} onSubmit={handleSumbit}>
                <label className={styles.Label}>
                    <div className={styles.Textbox}>
                        Login:
                    </div>
                    <input type="text" name="login" value={login} onChange={handleLoginChange} />
                </label>
                <br/>
                <label className={styles.Label}>
                    <div className={styles.Textbox}>
                        Password:
                    </div>
                    <input type="text" name="password" value={password} onChange={handlePasswordChange} />
                </label>
                <br />
                <div className={styles.Button}>
                    <input type="submit" value="Login" />
                </div>
            </form>
        </div>
    );
};

export default LoginComponent;