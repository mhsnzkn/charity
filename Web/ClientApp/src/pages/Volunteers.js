import React, { useEffect, useRef, useState } from 'react';
import ReactDatatable from '@ashvin27/react-datatable';

export default function Volunteers() {
    const [filter, setFilter] = useState(false);
    const [tableData, setTableData] = useState({total:0, records:{}})
    const filterDiv = useRef();
    useEffect(() =>{
        setFilter(true);
    },[])


    const columns = [
        {
            key: "name",
            text: "Name",
            className: "name",
            sortable: true
        },
        {
            key: "address",
            text: "Address",
            sortable: true
        },
        {
            key: "postcode",
            text: "Postcode",
            className: "postcode",
            sortable: true
        },
        {
            key: "rating",
            text: "Rating",
            className: "rating",
            sortable: true
        },
        {
            key: "type_of_food",
            text: "Type of Food",
            className: "type_of_food",
            sortable: true
        }
    ];
    const config = {
        page_size: 10,
        length_menu: [10, 20, 50],
        show_filter: true,
        show_pagination: true,
        button: {
            excel: false,
            print: false
        }
    }

    const tableChangeHandler = () => {
        //Get data
        console.log('getting data')
    }


    return (
        <>
            <h4>Volunteers</h4><hr />
            {/* <div className="custom-hidden-item"
            ref={filterDiv}
            style={
                filter ? {
                    height: filterDiv.current?.scrollHeight+"px"
                }
                : {
                    height: "0px"
                }
            } >
                <div class="form-group row p-1">
                    <label htmlFor="staticEmail" class="col-sm-2 col-form-label">Email</label>
                    <div class="col-sm-10">
                        <input type="text" readonly class="form-control" id="staticEmail" value="email@example.com"/>
                    </div>
                </div>

            </div>
            <div className='d-flex justify-content-end'>
                <button className='btn btn-dark m-2' onClick={() => setFilter(!filter)}>{filter === true ? "Hide" : "Show"} Filter</button>
            </div> */}

            <ReactDatatable
                config={config}
                records={tableData.records}
                columns={columns}
                dynamic={true}
                total_record={tableData.total}
                onChange={tableChangeHandler}/>
        </>
    );
}
