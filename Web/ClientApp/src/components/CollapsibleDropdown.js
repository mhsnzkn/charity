import React, { useState } from 'react';

export default function CollapsibleDropdown({title, items}) {
    const[isOpen, setIsOpen] = useState(false);

    return (
        <li>
            <a href="#vaultSubmenu" data-toggle="collapse" aria-expanded="false" className={`dropdown-toggle ${!isOpen && "collapsed"}`} onClick={()=>setIsOpen(!isOpen)}>{title}</a>
            <ul className={`collapse list-unstyled ${isOpen && "show"}`} >
                {items.map( i => {
                    return <li key={i.name}><a href={i.path}>{i.name}</a></li>
                })}
            </ul>
        </li>
    );
}
