import * as React from 'react';
import { FC, useState } from 'react';
import { useRestAPI } from '../../hooks/useRestAPI';
import { IForecastsComponentProps } from './IForecastsComponentProps';
import styles from './ForecastsComponent.module.scss';

const ForecastsComponent: FC<IForecastsComponentProps> = (props) => {

    //TO DO: exchangeForDownloadedData
    const data: { id:number, title:string }[] = [
        {
            id: 1,
            title: "test"
        },
        {
            id: 2,
            title: "test"
        },
        {
            id: 1,
            title: "test"
        },
        {
            id: 1,
            title: "test"
        }
    ]
    //TO DO: make headers data-aligned
    const headers: {key:string, label:string}[] = [
        { key: "id", label: "ID" },
        { key: "title", label: "Title" }
    ];

    return (
        <div className={styles.MForecastsComponent}>
            <table>
                <thead>
                    <tr>
                        {headers.map((row) => {
                            return <td key={row.key}>{row.label}</td>;
                        })}
                    </tr>
                </thead>
                <tbody>
                    {data.map((element) => {
                        return (
                            <tr key={element.id}>
                                <td>{element.id}</td>
                                <td>{element.title}</td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
};

export default ForecastsComponent;