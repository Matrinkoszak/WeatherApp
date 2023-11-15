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

    //TO DO: make headers data-aligned
    const headers: {key:string, label:string}[] = [
        { key: "Id", label: "ID" },
        { key: "avg_humidity", label: "Avarage Humidity [%]" },
        { key: "avg_temp", label: "Avarage Temperature [Celcius]" },
        { key: "avg_visibility", label: "Avarage Visibility [km]" },
        { key: "condition", label: "Condition" },
        { key: "date", label: "Date" },
        { key: "max_temp", label: "Maximum Temperature [Celcius]" },
        { key: "min_temp", label: "Minimal Temperature [Celcius]" },
        { key: "max_wind", label: "Maximum Wind Speed [km/h]" },
        { key: "moon_illumination", label: "Illumination of the Moon [%]" },
        { key: "moonphase", label: "Phase of the Moon" },
        { key: "moonrise", label: "Moonrise" },
        { key: "moonset", label: "Moonset" },
        { key: "sunrise", label: "Sunrise" },
        { key: "sunset", label: "Sunset" }
    ];

    return (
        <div className={styles.ForecastsComponent}>
            <table className={styles.table}>
                <thead>
                    <tr className={styles.tableHeader}>
                        {headers.map((row) => {
                            return <td key={row.key} className={styles.tableHeaderCell} >
                                <div className={styles.tableCell}>
                                    {row.label}
                                </div>
                            </td>;
                        })}
                    </tr>
                </thead>
                <tbody>
                    {forecastDays.map((element) => {
                        return (
                            <tr key={element.Id} className={styles.tableRow}>
                                <td>{element.Id}</td>
                                <td>{element.avg_humidity}</td>
                                <td>{element.avg_temp}</td>
                                <td>{element.avg_visibility}</td>
                                <td>{element.condition}</td>
                                <td>{element.date.toDateString()}</td>
                                <td>{element.max_temp}</td>
                                <td>{element.min_temp}</td>
                                <td>{element.max_wind}</td>
                                <td>{element.moon_illumination}</td>
                                <td>{element.moonphase}</td>
                                <td>{element.moonrise.toTimeString()}</td>
                                <td>{element.moonset.toTimeString()}</td>
                                <td>{element.sunrise.toTimeString()}</td>
                                <td>{element.sunset.toTimeString()}</td>
                            </tr>
                        );
                    })}
                </tbody>
            </table>
        </div>
    );
};

export default ForecastsComponent;