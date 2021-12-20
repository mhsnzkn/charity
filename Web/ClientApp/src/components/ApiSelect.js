import React from 'react';
import { useAxiosGet } from '../Hooks/HttpRequests';

export default function ApiSelect({ onChange, url, defaultValue = null, ...rest }) {
    const response = useAxiosGet(url)
    let options;
    let def;

    if (response.data)
        options = response.data.map(item => {
            return <option key={item.id} value={item.id}>{item.name}</option>
        })
    if (defaultValue) {
        def = <option value={defaultValue.id}>{defaultValue.name}</option>
    }

    return (
        <select
            {...rest}
            onChange={(e) => onChange(e.target.value)}
        >
            {def}
            {options}
        </select>
    );
}
