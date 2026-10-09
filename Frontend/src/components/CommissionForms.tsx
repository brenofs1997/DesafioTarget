import { useState, type FormEvent } from 'react';
import type { SaleRequest } from '../types';
import { FormHeader, SubmitButton } from './FormControls';

interface CommissionQueryFormProps {
	isLoading: boolean;
	onSubmit: () => void;
}

interface CommissionFormProps {
	isLoading: boolean;
	onSubmit: (request: SaleRequest) => void;
}

export function CommissionQueryForm({ isLoading, onSubmit }: CommissionQueryFormProps) {
	return (
		<form onSubmit={(event) => { event.preventDefault(); onSubmit(); }}>
			<FormHeader title="Consulta" description="Busque os resultados calculados pelo serviço." />
			<div className="query-callout">
				<span className="callout-symbol" aria-hidden="true">C</span>
				<div><strong>Consultar comissões</strong><span>GET · /Commissions/calculate</span></div>
			</div>
			<div className="form-actions"><SubmitButton isLoading={isLoading} /></div>
		</form>
	);
}

export function CommissionForm({ isLoading, onSubmit }: CommissionFormProps) {
	const [sales, setSales] = useState([{ id: 1, vendedor: '', valor: '' }]);

	function updateSale(id: number, field: 'vendedor' | 'valor', value: string) {
		setSales((current) => current.map((sale) => sale.id === id ? { ...sale, [field]: value } : sale));
	}

	function addSale() {
		setSales((current) => [...current, {
			id: Math.max(...current.map((sale) => sale.id)) + 1,
			vendedor: '',
			valor: '',
		}]);
	}

	function handleSubmit(event: FormEvent<HTMLFormElement>) {
		event.preventDefault();
		onSubmit({ vendas: sales.map(({ vendedor, valor }) => ({ vendedor, valor: Number(valor) })) });
	}

	return (
		<form onSubmit={handleSubmit}>
			<FormHeader title="Vendas" description="Informe cada vendedor e o valor da venda." />
			<div className="sales-list">
				{sales.map((sale, index) => (
					<div className="sale-row" key={sale.id}>
						<span className="row-number">{String(index + 1).padStart(2, '0')}</span>
						<label className="field">
							<span>Vendedor <b>*</b></span>
							<input value={sale.vendedor} onChange={(event) => updateSale(sale.id, 'vendedor', event.target.value)} placeholder="Nome do vendedor" required />
						</label>
						<label className="field">
							<span>Valor <b>*</b></span>
							<div className="input-with-prefix">
								<span>R$</span>
								<input type="number" min="0" step="0.01" value={sale.valor} onChange={(event) => updateSale(sale.id, 'valor', event.target.value)} placeholder="0,00" required />
							</div>
						</label>
						<button className="remove-sale" type="button" onClick={() => setSales((current) => current.filter((item) => item.id !== sale.id))} disabled={sales.length === 1} aria-label={`Remover venda ${index + 1}`} title="Remover venda">×</button>
					</div>
				))}
			</div>
			<div className="form-actions split-actions">
				<button className="text-button" type="button" onClick={addSale}><span aria-hidden="true">+</span> Adicionar venda</button>
				<SubmitButton isLoading={isLoading} />
			</div>
		</form>
	);
}