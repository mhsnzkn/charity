import React, { useRef, useState } from 'react';

export default function CollapsibleMenuItem({title, children}) {
    const[isOpen, setIsOpen] = useState(false);
    const ref = useRef();

    return (
        <li>
            <a href="#vaultSubmenu" data-toggle="collapse" aria-expanded="false" className="dropdown-toggle" onClick={()=>setIsOpen(!isOpen)}>{title}</a>
            <ul className="list-unstyled custom-hidden-item"
                ref={ref}
                style={
                    isOpen ? {
                        height: ref.current.scrollHeight+"px"
                    }
                    : {
                        height: "0px"
                    }
                } >
                {children}
            </ul>
        </li>
    );
}
