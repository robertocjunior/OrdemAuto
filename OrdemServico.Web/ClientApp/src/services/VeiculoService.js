import { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import apiConfig from '../Api';

export const VeiculoService = () => {

    const [state, setState] = useState({
        loading: false,
        error: null,
        success: false,
        mensagem: '',
        veiculos: [], // Deve começar como array vazio
        modal: { open: false, tipo: null, referencia: null, chavePrimaria: null, cadastros: []}
    });

    const carregarVeiculos = useCallback(async () => {
        setState(prev => ({ ...prev, loading: true, error: null })); // Limpe erros anteriores
        try {
            const response = await axios.get(`${apiConfig.veiculo.baseURL}${apiConfig.veiculo.endpoints.pesquisar}`);
            
            // Verificação de segurança: A resposta é um array?
            if (Array.isArray(response.data)) {
                setState(prev => ({ ...prev, veiculos: response.data, loading: false }));
            } else {
                // Se não for array, é um erro (provavelmente a API retornou HTML)
                console.error("Erro: A resposta da API não é um array.", response.data);
                setState(prev => ({ ...prev, error: true, mensagem: "Erro ao carregar: A API não retornou dados válidos.", loading: false, veiculos: [] }));
            }

        } catch (err) {
            console.error("Erro ao carregar veículos:", err);
            setState(prev => ({ ...prev, error: true, mensagem: "Erro ao carregar veículos.", loading: false, veiculos: [] }));
        }
    }, []);

    const cadastrarVeiculo = async (formData) => {
        setState(prev => ({ ...prev, loading: true, error: null }));
        try {
            const response = await axios.post(
                `${apiConfig.veiculo.baseURL}${apiConfig.veiculo.endpoints.adicionar}`,
                formData,
                { headers: { 'Content-Type': 'application/json' } }
            );

            await carregarVeiculos(); // Garante que a lista é recarregada
            fecharModal();
            setState(prev => ({ ...prev, success: true, mensagem: response.data?.mensagem || "Veículo cadastrado!", loading: false }));

        } catch (err) {
            console.error("Erro ao cadastrar veículo:", err);
            // O erro 405 provavelmente vem da configuração da baseURL no 'apiConfig'
            const msgErro = err.response?.status === 405 
                ? "Erro 405: Método não permitido. Verifique a baseURL da API."
                : "Erro ao cadastrar veículo";
            setState(prev => ({ ...prev, error: true, mensagem: msgErro, loading: false }));
        }
    };

    const editarVeiculo = async (formData, codigoVeiculo) => {
        setState(prev => ({ ...prev, loading: true, error: null }));
        try {
            const response = await axios.put(
                `${apiConfig.veiculo.baseURL}${apiConfig.veiculo.endpoints.editar}`,
                formData,
                { headers: { 'Content-Type': 'application/json' } }
            );

            await carregarVeiculos(); // Garante que a lista é recarregada
            fecharModal();
            setState(prev => ({ ...prev, success: true, mensagem: response.data?.mensagem || "Veículo atualizado!", loading: false }));

        } catch (err) {
            console.error("Erro ao editar Veículo:", err);
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
    }, [carregarVeiculos]); // Dependência correta no useEffect

    return {
        ...state,
        carregarVeiculos,
        abrirModal,
        fecharModal,
        cadastrarVeiculo,
        editarVeiculo
    };
};