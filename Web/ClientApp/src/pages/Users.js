import React from 'react';
import ReactDatatable from '@mkikets/react-datatable';
import { useState } from 'react/cjs/react.development';
import { Link } from 'react-router-dom';

export default function Users() {
  const [tableData, setTableData] = useState({ total: 0, records: [] });


  const columns = [
    {
      key: "name",
      text: "Name",
      className: "name",
    },
    {
      key: "address",
      text: "Address",
    },
    {
      key: "postcode",
      text: "Postcode",
      className: "postcode",
    },
    {
      key: "rating",
      text: "Rating",
      className: "rating",
    },
    {
      key: "_",
      text: "Action",
      cell: (record, index) => {
        return (
          <>
            <button className='btn btn-sm btn-danger'><i className='fas fa-trash'></i></button>
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

  const tableChangeHandler = () => {
    //Get data
    console.log('getting data')
  }

  return (
    <>
      <h4>Users</h4><hr />
      <div className='m-1 p-1'>
<Link to="/users/0" className='btn btn-primary'><i className='fa fa-plus'></i> Add</Link>
      </div>
      <ReactDatatable
        config={config}
        records={tableData.records}
        columns={columns}
        // dynamic={true}
        total_record={tableData.total}
        onChange={tableChangeHandler} />
    </>
  );
}
