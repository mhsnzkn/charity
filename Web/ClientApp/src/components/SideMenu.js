import React from 'react';
import CollapsibleDropdown from './CollapsibleDropdown';
import { RoutePaths } from '../Routes';

export default function SideMenu({menuActive}) {
    
    return (
        <nav id="sidebar" className={menuActive ? 'active' : ''}>
            <div className="sidebar-header">
                <a href="/#">
                    <img src="/logo_dark.png" className="w-100" alt="logo"/>
                </a>
            </div>

            <ul className="list-unstyled components">
                <li>
                    <a href="/#">Transfer</a>
                </li>
                    <li>
                        <a href="/#">Siparis</a>
                    </li>
                    <li>
                        <a href="/#">Rezervasyon</a>
                    </li>

                    <li>
                        <a href="#vaultSubmenu" data-toggle="collapse" aria-expanded="false" className="dropdown-toggle">Kasa İşlemleri</a>
                        <ul className="collapse list-unstyled" id="vaultSubmenu">
                            <li>
                                <a href="/#">Gelir</a>
                            </li>
                            <li>
                                <a href="/#">Gider</a>
                            </li>
                        </ul>
                    </li>
                    {/* <CollapsibleDropdown title="Diger Islemler" items={[{path:RoutePaths.Bir, name:"Biir"},{path:RoutePaths.Iki, name:"Ikki"}]}/> */}

                    <li>
                        <a href="/#">Rezervasyon</a>
                    </li>
            </ul>
        </nav>
    );
}
