import { useState, type FormEvent } from 'react';
import type { InterestRequest } from '../types';
import { FormHeader, SubmitButton } from './FormControls';

interface InterestFormProps {
	isLoading: boolean;
	onSubmit: (request: InterestRequest) => void;
}

export function InterestForm({ isLoading, onSubmit }: InterestFormProps) {
	const [value, setValue] = useState('');
	const [dueDate, setDueDate] = useState('');

	function handleSubmit(event: FormEvent<HTMLFormElement>) {
		event.preventDefault();
		onSubmit({ value: Number(value), dueDate });
	}

	return (
		<form onSubmit={handleSubmit}>
			<FormHeader title="Parâmetros" description="Preencha os dados para consultar o cálculo." />
			<div className="fields-stack">
				<label className="field">
					<span>Valor original <b>*</b></span>
					<div className="input-with-prefix">
						<span>R$</span>
						<input type="number" min="0" step="0.01" value={value} onChange={(event) => setValue(event.target.value)} placeholder="0,00" required />
					</div>
				</label>
				<label className="field">
					<span>Data de vencimento <b>*</b></span>
					<input type="date" value={dueDate} onChange={(event) => setDueDate(event.target.value)} required />
				</label>
			</div>
			<div className="form-actions"><SubmitButton isLoading={isLoading} /></div>
		</form>
	);
}