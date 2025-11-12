import Loading from '../components/ui/Loading';
import FlashMessage from '../components/ui/FlashMessage';
import { VeiculoService } from '../services/VeiculoService';
import { Modal } from "react-bootstrap";
import React, { useState } from 'react';
import VeiculoFormulario from '../components/veiculo/VeiculoFormulario';
import VeiculoTable from '../components/veiculo/VeiculoTable';

const VeiculoPage = () => {
    const { loading, error, success, mensagem, veiculos, modal, carregarVeiculos, abrirModal, fecharModal, cadastrarVeiculo, editarVeiculo } = VeiculoService();
    const [selected, setSelected] = useState([]);

    return (
        <div className="container">
            {success && <FlashMessage message={mensagem} type="success" duration={3000} />}
            {error && <FlashMessage message={mensagem} type="error" duration={3000} />}
            {loading ? (<Loading show={true} />) : (
                <><div className="d-flex justify-content-between align-items-end mb-4 mt-5">

                    {/* Título */}
                    <div>
                        <h1 className="fw-bold mb-1" style={{ color: "#158CBA" }}>
                            Cadastro de Veículos
                        </h1>
                        <p className="text-muted mb-0">
                            Gestão, visualização e cadastro de veículos.
                        </p>
                    </div>

                    {/* Data */}
                    <div className="text-end text-muted">
                        <small className="fw-semibold">
                            {new Date().toLocaleDateString('pt-BR', {
                                weekday: 'long',
                                day: '2-digit',
                                month: 'long',
                                year: 'numeric'
                            })}
                        </small>
                    </div>

                </div>

                    <div className="mt-4 mb-3 d-flex justify-content-between align-items-center">
                        <div className="d-flex align-items-center">
                            <button className="btn btn-primary me-2" onClick={() => abrirModal('nova', null)}>
                                <i className="fas fa-plus me-2"></i>Incluir
                            </button>

                            <button className="btn btn-secondary me-2" disabled={selected.length !== 1} onClick={() => {
                                const codigo = selected[0]?.codigo;
                                abrirModal('editar', codigo);
                            }}>
                                <i className="fas fa-pen me-2"></i>Editar
                            </button>
                        </div>

                        <div>
                            <button onClick={carregarVeiculos} className="btn btn-outline-secondary" disabled={loading}>
                                {loading ? 'Atualizando...' : 'Atualizar Lista'}
                            </button>
                        </div>
                    </div>

                    <VeiculoTable veiculos={veiculos} loading={loading} setSelected={setSelected} />

                    {modal.open && (
                        <Modal show={modal.open} onHide={fecharModal} centered size="lg" animation={true}>
                            <Modal.Header closeButton>
                                <Modal.Title>
                                    {modal.tipo === 'nova' && 'Nova Veiculo de Serviço'}
                                    {modal.tipo === 'editar' && 'Editar Veiculo de Serviço'}
                                </Modal.Title>
                            </Modal.Header>

                            <Modal.Body>
                                <VeiculoFormulario
                                    onSubmit={async (formData) => {
        
                                        const payload = {
                                            ...formData,
                                            codigo: Number(formData.codigo),
                                            nome: formData.nome,
                                            tipo: formData.tipo,
                                            cor: formData.cor,
                                            placa: formData.placa,
                                            mecanico: formData.mecanico,
                                            ano: formData.ano,
                                        };

                                        if (modal.tipo == 'nova') {
                                            await cadastrarVeiculo(payload);
                                        } else {
                                            await editarVeiculo(payload, modal.chavePrimaria);
                                        }
                                        fecharModal();
                                        carregarVeiculos();
                                    }}

                                    onCancel={fecharModal}
                                    veiculo={modal.referencia}
                                />

                            </Modal.Body>
                        </Modal>
                    )}
                </>
            )}
        </div>
    );
};

export default VeiculoPage;
