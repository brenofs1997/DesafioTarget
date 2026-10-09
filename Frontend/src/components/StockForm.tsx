import { useState, type FormEvent } from 'react';
import type { StockMovementRequest, StockMovementType } from '../types';
import { FormHeader, SubmitButton } from './FormControls';

interface StockFormProps {
	isLoading: boolean;
	onSubmit: (request: StockMovementRequest) => void;
}

export function StockForm({ isLoading, onSubmit }: StockFormProps) {
	const [productCode, setProductCode] = useState('');
	const [quantity, setQuantity] = useState('');
	const [type, setType] = useState<StockMovementType | ''>('');
	const [description, setDescription] = useState('');

	function handleSubmit(event: FormEvent<HTMLFormElement>) {
		event.preventDefault();
		if (!type) return;

		onSubmit({
			productCode: Number(productCode),
			quantity: Number(quantity),
			type,
			description,
		});
	}

	return (
		<form onSubmit={handleSubmit}>
			<FormHeader title="Dados da movimentação" description="Preencha os dados do produto e da operação." />
			<div className="fields-grid">
				<label className="field">
					<span>Código do produto <b>*</b></span>
					<input type="number" min="1" step="1" value={productCode} onChange={(event) => setProductCode(event.target.value)} placeholder="Ex.: 1024" required />
				</label>
				<label className="field">
					<span>Quantidade <b>*</b></span>
					<input type="number" min="1" step="1" value={quantity} onChange={(event) => setQuantity(event.target.value)} placeholder="0" required />
				</label>
				<label className="field">
					<span>Tipo <b>*</b></span>
					<select value={type} onChange={(event) => setType(event.target.value as StockMovementType | '')} required>
						<option value="" disabled>Selecione o tipo</option>
						<option value="Entry">Entrada</option>
						<option value="Exit">Saída</option>
					</select>
				</label>
				<label className="field field-wide">
					<span>Descrição <b>*</b></span>
					<input value={description} onChange={(event) => setDescription(event.target.value)} placeholder="Ex.: Reposição de estoque" required />
				</label>
			</div>
			<div className="form-actions"><SubmitButton isLoading={isLoading} /></div>
		</form>
	);
}