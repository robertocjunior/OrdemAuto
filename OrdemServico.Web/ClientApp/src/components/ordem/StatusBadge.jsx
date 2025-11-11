// src/components/StatusBadge.jsx
import React from "react";

const statusStyles = {
    "Cancelado": { text: "Cancelado", class: "badge bg-danger" },
    "EmAndamento": { text: "Em andamento", class: "badge bg-warning" },
    "Pendente": { text: "Pendente", class: "badge bg-info" },
    "Concluido": { text: "Concluído", class: "badge bg-success" },
};

const StatusBadge = ({ value }) => {
    const st = statusStyles[value] || { text: "Desconhecido", class: "bg-gray-100 text-gray-600" };

    return (
        <>
        <span className={`px-3 py-1 rounded-full text-sm font-medium ${st.class}`}>
            {st.text}
        </span>
        </>
    );
};

export default StatusBadge;