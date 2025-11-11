import React, { useState, useEffect } from "react";
import { Form, Button, Row, Col, Table, Collapse } from "react-bootstrap";

const OrdemFormulario = ({ onSubmit, onCancel, ordem, cadastros }) => {

    // FORM PRINCIPAL
    const [formData, setFormData] = useState({
        descricao: "",
        dataOrdem: "",
        dataRetorno: "",
        codigoPrestador: "",
        codigoSeguradora: "",
        codigoVeiculo: "",
        observacao: "",
        valorTotal: 0
    });

    // ITENS DE PEÇAS
    const [itens, setItens] = useState([]);

    // FORM NOVO ITEM
    const [itemForm, setItemForm] = useState({
        codigoPeca: "",
        descricaoReparo: "",
        valorEstimado: "",
        valorReal: "",
        status: "EmAndamento",
        usarNovaPeca: false,
        novaPeca: {
            modelo: "",
            valor: ""
        }
    });

    const toggleStatusItem = (index) => {
        setItens(prev =>
            prev.map((item, i) =>
                i === index
                    ? {
                        ...item,
                        status: item.status === "EmAndamento"
                            ? "Concluido"
                            : "EmAndamento"
                    }
                    : item
            )
        );
    };

    // Quando editar ordem
    useEffect(() => {
        if (ordem) {
            setFormData({
                descricao: ordem.descricao,
                dataOrdem: ordem.dataOrdem?.substring(0, 10),
                dataRetorno: ordem.dataRetorno?.substring(0, 10),
                codigoPrestador: ordem.prestador?.codigo || "",
                codigoSeguradora: ordem.seguradora?.codigo || "",
                codigoVeiculo: ordem.codigoVeiculo || "",
                observacao: ordem.observacao,
                valorTotal: ordem.valorTotal
            });

            setItens(ordem.itens || []);
        }
    }, [ordem]);

    // HANDLERS DO FORM PRINCIPAL
    const handleMainChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    // HANDLERS DO FORM DE ITENS
    const handleItemChange = (e) => {
        const { name, value } = e.target;

        if (name.startsWith("novaPeca.")) {
            const field = name.replace("novaPeca.", "");
            setItemForm(prev => ({
                ...prev,
                novaPeca: { ...prev.novaPeca, [field]: value }
            }));
        } else {
            setItemForm(prev => ({ ...prev, [name]: value }));
        }
    };

    // ADICIONAR ITEM
    const adicionarItem = () => {
        if (!itemForm.usarNovaPeca && !itemForm.codigoPeca) {
            alert("Selecione ou cadastre uma peça.");
            return;
        }

        const novoItem = {
            codigoPeca: itemForm.usarNovaPeca ? 0 : Number(itemForm.codigoPeca),
            descricaoReparo: itemForm.descricaoReparo,
            valorEstimado: Number(itemForm.valorEstimado),
            valorReal: Number(itemForm.valorReal),
            status: itemForm.status,
            novaPeca: itemForm.usarNovaPeca
                ? { ...itemForm.novaPeca, valor: Number(itemForm.novaPeca.valor) }
                : null
        };

        setItens(prev => [...prev, novoItem]);

        // resetar
        setItemForm({
            codigoPeca: "",
            descricaoReparo: "",
            valorEstimado: "",
            valorReal: "",
            status: "EmAndamento",
            usarNovaPeca: false,
            novaPeca: { modelo: "", valor: "" }
        });
    };

    const removerItem = (i) => {
        setItens(prev => prev.filter((_, index) => index !== i));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (itens.length === 0) {
            alert("Adicione ao menos 1 peça à OS.");
            return;
        }

        // ENVIA PARA ORDEM PAGE
        onSubmit(formData, itens);
    };

    return (
        <Form onSubmit={handleSubmit}>

            {/* DADOS GERAIS */}
            <Row className="mb-3">
                <Col md={6}>
                    <Form.Group>
                        <Form.Label>Descrição</Form.Label>
                        <Form.Control
                            type="text"
                            name="descricao"
                            value={formData.descricao}
                            onChange={handleMainChange}
                        />
                    </Form.Group>
                </Col>

                <Col md={3}>
                    <Form.Group>
                        <Form.Label>Data Ordem</Form.Label>
                        <Form.Control
                            type="date"
                            name="dataOrdem"
                            value={formData.dataOrdem}
                            onChange={handleMainChange}
                        />
                    </Form.Group>
                </Col>

                <Col md={3}>
                    <Form.Group>
                        <Form.Label>Data Retorno</Form.Label>
                        <Form.Control
                            type="date"
                            name="dataRetorno"
                            value={formData.dataRetorno}
                            onChange={handleMainChange}
                        />
                    </Form.Group>
                </Col>
            </Row>

            {/* PRESTADOR / SEGURADORA / VEÍCULO */}
            <h5 className="mt-4">Informações Gerais</h5>

            <Row className="mb-3">

                {/* Prestador */}
                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Prestador</Form.Label>
                        <Form.Select
                            name="codigoPrestador"
                            value={formData.codigoPrestador}
                            onChange={handleMainChange}
                        >
                            <option value="">Selecione...</option>

                            {cadastros?.prestadora   ?.map(p => (
                                <option key={p.codigo} value={p.codigo}>
                                    {p.nome}
                                </option>
                            ))}

                        </Form.Select>
                    </Form.Group>
                </Col>

                {/* Seguradora */}
                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Seguradora</Form.Label>
                        <Form.Select
                            name="codigoSeguradora"
                            value={formData.codigoSeguradora}
                            onChange={handleMainChange}
                        >
                            <option value="">Selecione...</option>

                            {cadastros?.seguradora?.map(s => (
                                <option key={s.codigo} value={s.codigo}>
                                    {s.nome}
                                </option>
                            ))}

                        </Form.Select>
                    </Form.Group>
                </Col>

                {/* Veículo */}
                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Veículo</Form.Label>
                        <Form.Select
                            name="codigoVeiculo"
                            value={formData.codigoVeiculo}
                            onChange={handleMainChange}
                        >
                            <option value="">Selecione...</option>

                            {cadastros?.veiculo?.map(v => (
                                <option key={v.codigo} value={v.codigo}>
                                    {v.nome} - {v.placa}
                                </option>
                            ))}

                        </Form.Select>
                    </Form.Group>
                </Col>
            </Row>


            {/* --- FORM DE ITENS --- */}

            <h5 className="mt-4">Peças e Reparos</h5>

            <Row>
                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Peça Existente</Form.Label>
                        <Form.Select
                            disabled={itemForm.usarNovaPeca}
                            name="codigoPeca"
                            value={itemForm.codigoPeca}
                            onChange={handleItemChange}
                        >
                            <option value="">Selecione...</option>
                            {cadastros?.pecas?.map(p => (
                                <option key={p.codigo} value={p.codigo}>
                                    {p.nome}
                                </option>
                            ))}                        </Form.Select>
                    </Form.Group>
                </Col>

                <Col md={4}>
                    <Button
                        className="mt-4"
                        variant="outline-primary"
                        onClick={() =>
                            setItemForm(prev => ({ ...prev, usarNovaPeca: !prev.usarNovaPeca }))
                        }
                    >
                        {itemForm.usarNovaPeca ? "Cancelar peça nova" : "Cadastrar nova peça"}
                    </Button>
                </Col>
            </Row>

            <Collapse in={itemForm.usarNovaPeca}>
                <div className="p-3 border rounded mt-3">
                    <Form.Group className="mb-3">
                        <Form.Label>Peça</Form.Label>
                        <Form.Control
                            type="text"
                            name="novaPeca.modelo"
                            value={itemForm.novaPeca.modelo}
                            onChange={handleItemChange}
                        />
                    </Form.Group>

                    <Form.Group>
                        <Form.Label>Valor</Form.Label>
                        <Form.Control
                            type="number"
                            name="novaPeca.valor"
                            value={itemForm.novaPeca.valor}
                            onChange={handleItemChange}
                        />
                    </Form.Group>
                </div>
            </Collapse>

            <Row className="mt-3">
                <Col md={6}>
                    <Form.Group>
                        <Form.Label>Descrição do Reparo</Form.Label>
                        <Form.Control
                            type="text"
                            name="descricaoReparo"
                            value={itemForm.descricaoReparo}
                            onChange={handleItemChange}
                        />
                    </Form.Group>
                </Col>

                <Col md={3}>
                    <Form.Group>
                        <Form.Label>Valor Estimado</Form.Label>
                        <Form.Control
                            type="number"
                            name="valorEstimado"
                            value={itemForm.valorEstimado}
                            onChange={handleItemChange}
                        />
                    </Form.Group>
                </Col>

                <Col md={3}>
                    <Form.Group>
                        <Form.Label>Valor Real</Form.Label>
                        <Form.Control
                            type="number"
                            name="valorReal"
                            value={itemForm.valorReal}
                            onChange={handleItemChange}
                        />
                    </Form.Group>
                </Col>
            </Row>

            <Button className="mt-3" onClick={adicionarItem}>
                Adicionar Item
            </Button>

            {/* LISTA DE ITENS */}
            {itens.length > 0 && (
                <Table bordered size="sm" className="mt-3">
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Peça</th>
                            <th>Reparo</th>
                            <th>Estimado</th>
                            <th>Real</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {itens.map((i, idx) => (
                            <tr key={idx}>
                                <td>{idx + 1}</td>
                                <td>
                                    {i.codigoPeca > 0
                                        ? `ID ${i.codigoPeca}`
                                        : `Nova Peça: ${i.novaPeca.modelo}`}
                                </td>
                                <td>{i.descricaoReparo}</td>
                                <td>R$ {i.valorEstimado}</td>
                                <td>R$ {i.valorReal}</td>
                                <td className="d-flex gap-2">
                                    <Button
                                        size="sm"
                                        variant={i.status === "EmAndamento" ? "warning" : "success"}
                                        onClick={() => toggleStatusItem(idx)}
                                    >
                                        {i.status === "EmAndamento" ? "Em andamento" : "Concluído"}
                                    </Button>

                                    <Button
                                        size="sm"
                                        variant="danger"
                                        onClick={() => removerItem(idx)}
                                    >
                                        Remover
                                    </Button>
                                </td>

                            </tr>
                        ))}
                    </tbody>
                </Table>
            )}

            <div className="d-flex justify-content-end mt-4">
                <Button variant="secondary" className="me-2" onClick={onCancel}>Cancelar</Button>
                <Button variant="primary" type="submit">Salvar OS</Button>
            </div>

        </Form>
    );
};

export default OrdemFormulario;
