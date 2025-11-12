import React from "react";
import { NavLink } from "react-router-dom";

const Sidebar = () => {
    const navItems = [
        { path: "/dashboard", icon: "fa-home", text: "Dashboard" },
        { path: "/ordem", icon: "fa-file-invoice", text: "Ordem de Serviço" },
        { path: "/veiculos", icon: "fa-file-invoice", text: "Veículos" },
    ];

    return (
        <div className="d-flex flex-column flex-shrink-0 p-3 bg-light border-end"
            style={{ width: "250px", minHeight: "100vh" }}>

            {/* LOGO / TÍTULO */}
            <a href="/" className="d-flex align-items-center mb-4 mb-md-0 me-md-auto link-dark text-decoration-none">
                <span className="fs-5 fw-semibold">Auto Ordem</span>
            </a>

            <hr />

            {/* NAV LINKS */}
            <ul className="nav nav-pills flex-column mb-auto">

                {navItems.map((item, i) => (
                    <li className="nav-item" key={i}>
                        <NavLink
                            to={item.path}
                            className={({ isActive }) =>
                                `nav-link d-flex align-items-center gap-2 my-1 
                                 ${isActive ? "active bg-primary text-white" : "link-dark"}`
                            }
                        >
                            <i className={`fas ${item.icon}`}></i>
                            {item.text}
                        </NavLink>
                    </li>
                ))}

            </ul>

            <hr />

            {/* FOOTER */}
            <div className="mt-auto">
                <button className="btn btn-outline-primary w-100 d-flex align-items-center justify-content-center gap-2">
                    <i className="fas fa-sign-out-alt"></i>
                    Sair
                </button>
            </div>
        </div>
    );
};

export default Sidebar;
