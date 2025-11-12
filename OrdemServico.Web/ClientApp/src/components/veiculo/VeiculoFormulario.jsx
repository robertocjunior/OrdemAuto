import React, { useState, useEffect } from "react";
import { Form, Button, Row, Col } from "react-bootstrap";

const VeiculoFormulario = ({ onSubmit, onCancel, veiculo }) => {

    const [formData, setFormData] = useState({
        codigo: 0,
        nome: "",
        tipo: "",
        cor: "",
        placa: "",
        mecanico: "",
        ano: ""
    });

    useEffect(() => {
        if (veiculo) {
            setFormData({
                codigo: veiculo.codigo || 0,
                nome: veiculo.nome || "",
                tipo: veiculo.tipo || "",
                cor: veiculo.cor || "",
                placa: veiculo.placa || "",
                mecanico: veiculo.mecanico || "",
                ano: veiculo.ano
                    ? new Date(veiculo.ano).toISOString().slice(0, 10)
                    : ""
            });
        }
    }, [veiculo]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const payload = {
            ...formData,
            ano: formData.ano ? new Date(formData.ano) : null
        };

        onSubmit(payload);
    };

    return (
        <Form onSubmit={handleSubmit}>
            <h4 className="mb-3">Cadastro de Veículo</h4>

            <Row className="mb-3">
                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Código</Form.Label>
                        <Form.Control
                            type="text"
                            name="codigo"
                            value={formData.codigo}
                            disabled={true}
                        />
                    </Form.Group>
                </Col>

                <Col md={6}>
                    <Form.Group>
                        <Form.Label>Nome</Form.Label>
                        <Form.Control
                            type="text"
                            name="nome"
                            value={formData.nome}
                            onChange={handleChange}
                        />
                    </Form.Group>
                </Col>

                <Col md={2}>
                    <Form.Group>
                        <Form.Label>Tipo</Form.Label>
                        <Form.Control
                            type="text"
                            name="tipo"
                            value={formData.tipo}
                            onChange={handleChange}
                        />
                    </Form.Group>
                </Col>
            </Row>

            <Row className="mb-3">
                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Cor</Form.Label>
                        <Form.Control
                            type="text"
                            name="cor"
                            value={formData.cor}
                            onChange={handleChange}
                        />
                    </Form.Group>
                </Col>

                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Placa</Form.Label>
                        <Form.Control
                            type="text"
                            name="placa"
                            value={formData.placa}
                            onChange={handleChange}
                        />
                    </Form.Group>
                </Col>

                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Mecânico</Form.Label>
                        <Form.Control
                            type="text"
                            name="mecanico"
                            value={formData.mecanico}
                            onChange={handleChange}
                        />
                    </Form.Group>
                </Col>
            </Row>

            <Row className="mb-3">
                <Col md={4}>
                    <Form.Group>
                        <Form.Label>Ano</Form.Label>
                        <Form.Control
                            type="date"
                            name="ano"
                            value={formData.ano}
                            onChange={handleChange}
                        />
                    </Form.Group>
                </Col>
            </Row>

            <div className="d-flex justify-content-end mt-4">
                <Button variant="secondary" className="me-2" onClick={onCancel}>
                    Cancelar
                </Button>

                <Button variant="primary" type="submit">
                    Salvar Veículo
                </Button>
            </div>
        </Form>
    );
};

export default VeiculoFormulario;
