import React, { useState } from 'react';
import { Link } from 'react-router-dom';

export default function Users() {
  const [tableData, setTableData] = useState({ total: 0, records: [] })




  return (
    <>
      <h4>Users</h4><hr />
      <div className='m-1 p-1'>
        <Link to="/Users/Edit/0" className='btn btn-primary'><i className='fa fa-plus'></i> Add</Link>
      </div>


    </>
  );
}
