import * as React from 'react';
import { FC, useState, useEffect } from 'react';
import { useRestAPI } from '../../hooks/useRestAPI';
import { IForecastsComponentProps } from './IForecastsComponentProps';
import styles from './ForecastsComponent.module.scss';
import { ForecastDay } from '../../models/ForecastDayModel';

const ForecastsComponent: FC<IForecastsComponentProps> = (props) => {
    const { getForecastDays } = useRestAPI();
    const [ forecastDays, setForecastDays ] = useState<ForecastDay[]>([]);


    useEffect(() => {
        getForecastDays(props.token, "Poznan", new Date("2022-05-20 00:00:00"), new Date("2022-05-30 00:00:00")).then(resp => {
            setForecastDays(resp as ForecastDay[]);
            }
        )
    }, [])

    //TO DO: exchangeForDownloadedData
    /*const data: { id:number, title:string }[] = [
        {
            id: 1,
            title: "test"
        },
        {
            id: 2,
            title: "test"
        },
        {
            id: 3,
            title: "test"
        },
        {
            id: 4,
            title: "test"
        }
    ]*/

    //TO DO: make headers data-aligned
    const headers: {key:string, label:string}[] = [
        { key: "Id", label: "ID" },
        { key: "avg_humidity", label: "Avarage Humidity [%]" },
        { key: "avg_temp", label: "Avarage Temperature [Celcius]" },
        { key: "avg_visibility", label: "Avarage Visibility [km]" },
        { key: "condition", label: "Condition" },
        { key: "date", label: "Date" },
        { key: "max_temp", label: "Maximum Tempreature [Celcius]" },
        { key: "max_wind", label: "Maximal Wind Speed [km/h]" },
        { key: "min_temp", label: "Minimal Temperature [Celcius]" },
        { key: "moon_illumination", label: "Illumination of the Moon [%]" },
        { key: "moonphase", label: "Phase of the Moon" },
        { key: "moonrise", label: "Moonrise" },
        { key: "moonset", label: "Moonset" },
        { key: "sunrise", label: "Sunrise" },
        { key: "sunset", label: "Sunset" }
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
                    {forecastDays.map((element) => {
                        return (
                            <tr key={element.Id}>
                                <td>{element.Id}</td>
                                <td>{element.date.toString()}</td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
};

export default ForecastsComponent;