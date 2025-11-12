import React, { useMemo, useState, useEffect } from 'react';
import { AgGridReact } from 'ag-grid-react';

import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import '../../styles/ag-custom.css';

const VeiculoTable = ({ veiculos, loading, setSelected }) => {
    const [gridApi, setGridApi] = useState(null);

    const columns = useMemo(() => [
        {
            headerCheckboxSelection: true,
            checkboxSelection: true,
            headerName: "",
            width: 50,
            pinned: 'left',
            suppressMenu: true,
            sortable: false, // Correção
            filter: false     // Correção
        },

        {
            headerName: "#",
            field: "codigo",
            width: 90,
            cellRenderer: "agGroupCellRenderer"
        },

        {
            headerName: "Nome",
            field: "nome",
            width: 200
        },

        {
            headerName: "Tipo",
            field: "tipo",
            width: 150
        },

        {
            headerName: "Cor",
            field: "cor",
            width: 150
        },

        {
            headerName: "Placa",
            field: "placa",
            width: 150
        },

        {
            headerName: "Mecânico",
            field: "mecanico",
            width: 200
        },

        {
            headerName: "Ano",
            field: "ano",
            width: 150,
            valueFormatter: (p) =>
                p.value ? new Date(p.value).getFullYear() : ""
        }

    ], []);

    useEffect(() => {
        if (gridApi && veiculos?.length) {
            gridApi.sizeColumnsToFit();
        }
        // console.log(veiculos); // Removido ou comentado para limpar o console
    }, [gridApi, veiculos]);

    return (
        <div className="ag-theme-alpine custom" style={{ height: 600, width: '100%' }}>
            <AgGridReact
                rowData={veiculos}
                columnDefs={columns}
                defaultColDef={{
                    flex: 1,
                    sortable: true,
                    filter: true,
                    resizable: true
                }}
                rowSelection="multiple"
                animateRows={true}
                onGridReady={(params) => setGridApi(params.api)}
                onSelectionChanged={(event) => {
                    const selectedNodes = event.api.getSelectedNodes();
                    const selectedData = selectedNodes.map(n => n.data);
                    setSelected(selectedData);
                }}
            />
        </div>
    );
};

export default VeiculoTable;