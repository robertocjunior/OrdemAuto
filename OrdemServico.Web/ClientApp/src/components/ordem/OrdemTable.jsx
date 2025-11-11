import React, { useMemo, useState, useEffect } from 'react';
import { AgGridReact } from 'ag-grid-react';
import 'ag-grid-community/styles/ag-grid.css';
import 'ag-grid-community/styles/ag-theme-alpine.css';
import '../../styles/ag-custom.css';
import { ModuleRegistry } from 'ag-grid-community';
import { MasterDetailModule } from 'ag-grid-enterprise';
import StatusBadge from './StatusBadge';

// Registrar módulo do MasterDetail
ModuleRegistry.registerModules([MasterDetailModule]);

const OrdemTable = ({ ordens, loading, setSelected }) => {
    const [gridApi, setGridApi] = useState(null);

    // COLUNAS DO GRID PRINCIPAL - adaptadas pro DTOOrdemServicoResponse
    const mainColumns = useMemo(() => [
        { headerCheckboxSelection: true, checkboxSelection: true, headerName: "", field: "checkbox", width: 50, pinned: 'left', suppressMenu: true, suppressSorting: true, suppressFilter: true },

        { headerName: "#", field: "codigo", cellRenderer: "agGroupCellRenderer", width: 90 },

        { headerName: "Descrição", field: "descricao", width: 200 },

        {
            headerName: "Data da OS", field: "dataOrdem", width: 150,
            valueFormatter: p => p.value ? new Date(p.value).toLocaleDateString() : ""
        },

        {
            headerName: "Retorno", field: "dataRetorno", width: 150,
            valueFormatter: p => p.value ? new Date(p.value).toLocaleDateString() : ""
        },

        { headerName: "Prestador", field: "prestador.nome", width: 200 },

        { headerName: "Seguradora", field: "seguradora.nome", width: 200 },

        { headerName: "Veículo", field: "veiculo.placa", width: 200 },

        {
            headerName: "Valor Total", field: "valorTotal", width: 150,
            valueFormatter: p => p.value?.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })
        }
    ], []);

    // COLUNAS DO SUBGRID (Itens da Ordem)
    // COLUNAS DO SUBGRID (Itens da Ordem)
    const detailColumns = useMemo(() => [
        { headerName: "Cod. Peça", field: "codigo", width: 120 },

        { headerName: "Peça (Modelo)", field: "peca.modelo", width: 200 },

        { headerName: "Descrição do Reparo", field: "descricaoReparo", width: 300 },

        {
            headerName: "Valor Real",
            field: "valorReal",
            width: 150,
            valueFormatter: p =>
                p.value?.toLocaleString("pt-BR", {
                    style: "currency",
                    currency: "BRL"
                })
        },

        {
            headerName: "Status",
            field: "status",
            width: 150,
            cellRenderer: (params) => <StatusBadge value={params.value} />
        }

    ], []);


    // CONFIG SUBGRID
    const detailCellRendererParams = useMemo(() => ({
        detailGridOptions: {
            columnDefs: detailColumns,
            defaultColDef: {
                flex: 1,
                sortable: true,
                filter: true,
                resizable: true
            },
            animateRows: true
        },
        getDetailRowData: (params) => {
            params.successCallback(params.data.itens || []);
        }
    }), [detailColumns]);

    useEffect(() => {
        if (gridApi && ordens.length) {
            gridApi.sizeColumnsToFit();
        }
    }, [gridApi, ordens]);

    return (
        <div className="ag-theme-alpine custom" style={{ height: 600, width: '100%' }}>
            <AgGridReact
                rowData={ordens}
                columnDefs={mainColumns}
                defaultColDef={{
                    flex: 1,
                    sortable: true,
                    filter: true,
                    resizable: true
                }}
                masterDetail={true}
                detailCellRendererParams={detailCellRendererParams}
                rowSelection="multiple"
                onGridReady={(params) => setGridApi(params.api)}
                onSelectionChanged={(event) => {
                    const selectedNodes = event.api.getSelectedNodes();
                    const selectedData = selectedNodes.map(node => node.data);
                    setSelected(selectedData);
                }}
                animateRows={true}
            />
        </div>
    );
};

export default OrdemTable;
