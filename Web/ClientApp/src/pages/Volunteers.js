import React, { useEffect, useState } from 'react';
import ReactDatatable from '@mkikets/react-datatable';
import { useAxiosGet } from '../Hooks/HttpRequests';

export default function Volunteers() {
    const [tableData, setTableData] = useState({ total: 0, records: [] })

    const response = useAxiosGet('/api/volunteer');

    useEffect(()=>{
        if(response.data){
            setTableData({total:response.data.total, records:response.data.records})
        }
    },[response.data])
    


    const columns = [
        {
            key: "name",
            text: "Name",
            align: 'center'
        },
        {
            key: "email",
            text: "Email",
            className:'text-center',
            align: 'center'
        },
        {
            key: "mobileNumber",
            text: "Mobile Number",
            align: 'center'
        },
        {
            key: "id",
            text: "Action",
            align: 'center',
            className: 'text-center',
            cell: (record, index) => {
                return (
                    <>
                        <button className='btn btn-sm btn-success m-1' onClick={()=> console.log(record.id)}><i className='fa fa-check'></i></button>
                        <button className='btn btn-sm btn-info m-1' onClick={()=> console.log(record.id)}><i className='fa fa-list'></i></button>
                        <button className='btn btn-sm btn-danger m-1' onClick={()=> console.log(record.id)}><i className='fa fa-trash'></i></button>
                    </>
                )
            }
        }
    ];
    const config = {
        page_size: 10,
        length_menu: [10, 20, 50],
        show_filter: true,
        show_pagination: true,
        pagination: 'advance',
        filename: "Volunteers",
        button: {
            excel: true,
            print: true
        }
    }

    const tableChangeHandler = data => {
        let queryString = Object.keys(data).map((key) => {
            if(key === "sort_order" && data[key]){
                return encodeURIComponent("sort_order") + '=' + encodeURIComponent(data[key].order) + '&' + encodeURIComponent("sort_column") + '=' + encodeURIComponent(data[key].column)
            } else {
                return encodeURIComponent(key) + '=' + encodeURIComponent(data[key])
            }
            
        }).join('&');

        //this.getData(queryString);
        console.log(queryString);
    }

    const getData = (queryString = "") => {
        let url = "http://tradekisan.in/react-datatable/sampleAPI/?" + queryString
        fetch(url, {
            headers: {
                "Accept": "application/json"
            }
        })
        .then(res => res.json())
        .then(res => {
            this.setState({
                total: res.total,
                records: res.result
            })
        })

    }


    return (
        <>
            <h4>Volunteers</h4><hr />

            <ReactDatatable
                config={config}
                records={tableData.records}
                columns={columns}
                 dynamic={true}
                total_record={tableData.total}
                onChange={tableChangeHandler} />
        </>
    );
}
