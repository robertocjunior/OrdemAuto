import { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import apiConfig from '../Api';

export const VeiculoService = () => {

    const [state, setState] = useState({
        loading: false,
        error: null,
        success: false,
        mensagem: '',
        veiculos: [],
        modal: { open: false, tipo: null, referencia: null, chavePrimaria: null, cadastros: []}
    });

    const carregarVeiculos = useCallback(async () => {
        setState(prev => ({ ...prev, loading: true }));
        try {
            const response = await axios.get(`${apiConfig.veiculo.baseURL}${apiConfig.veiculo.endpoints.pesquisar}`);
            setState(prev => ({ ...prev, veiculos: response.data, loading: false }));
        } catch (err) {
            setState(prev => ({ ...prev, error: true, mensagem: "Erro ao carregar ordens", loading: false }));
        }
    }, []);

    const cadastrarVeiculo = async (formData) => {
        setState(prev => ({ ...prev, loading: true }));
        try {
            const response = await axios.post(
                `${apiConfig.veiculo.baseURL}${apiConfig.veiculo.endpoints.adicionar}`,
                formData,
                { headers: { 'Content-Type': 'application/json' } }
            );

            carregarVeiculos();
            fecharModal();
            setState(prev => ({ ...prev, success: true, mensagem: response.data?.mensagem || "Veículo cadastrada!", loading: false }));

        } catch (err) {
            setState(prev => ({ ...prev, error: true, mensagem: "Erro ao cadastrar veículo", loading: false }));
        }
    };

    const editarVeiculo = async (formData, codigoVeiculo) => {
        setState(prev => ({ ...prev, loading: true }));
        try {
            const response = await axios.put(
                `${apiConfig.veiculo.baseURL}${apiConfig.veiculo.endpoints.editar}`,
                formData,
                { headers: { 'Content-Type': 'application/json' } }
            );

            carregarVeiculos();
            fecharModal();
            setState(prev => ({ ...prev, success: true, mensagem: response.data?.mensagem || "Veículo atualizado!", loading: false }));

        } catch (err) {
            setState(prev => ({ ...prev, error: true, mensagem: "Erro ao editar Veículo", loading: false }));
        }
    };

    const abrirModal = (tipo, codigo) => {
        if (tipo === 'nova') {
            setState(prev => ({
                ...prev,
                modal: { open: true, tipo: 'nova', referencia: null, chavePrimaria: null }
            }));
        } else if (tipo === 'editar') {
            const veiculo = state.veiculos.find(o => o.codigo === codigo);

            setState(prev => ({
                ...prev,
                modal: { open: true, tipo: 'editar', referencia: veiculo, chavePrimaria: veiculo.codigo }
            }));
        }
            
    };

    const fecharModal = () => {
        setState(prev => ({ ...prev, modal: { open: false, tipo: null, referencia: null } }));
    };

    useEffect(() => {
        carregarVeiculos();
    }, []);

    return {
        ...state,
        carregarVeiculos,
        abrirModal,
        fecharModal,
        cadastrarVeiculo,
        editarVeiculo
    };
};
