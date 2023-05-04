import { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { useParams } from 'react-router'
import { ConsoleGet, ConsoleUpdate } from '../../models/Console'
import { useForm } from 'react-hook-form'
import { getConsole, updateConsole } from '../../api/ConsolesApi'
import Button from '../ui/Button'

interface Props {
    name: string
    description: string
    dailyPrice: number
}

function ConsoleInfoUpdateForm() {
    const { t } = useTranslation()
    const { id } = useParams()
    const [consoleGet, setConsole] = useState<ConsoleGet>()
    const {
        register,
        watch,
        handleSubmit,
        formState: { errors },
        setValue
    } = useForm<Props>()
    const [error, setError] = useState('')

    useEffect(() => {
        getConsole(parseInt(id ?? '-1')).then((response) => {
            setConsole(response.data)
            setValue('name', response.data.name ?? '')
            setValue('description', response.data.description ?? '')
            setValue('dailyPrice', response.data.dailyPrice ?? 0)
        })
    }, [])

    return (
        <form className="select-none bg-bg-primary text-t-primary mx-auto">
            <div className="font-bold text-left">{t('consoleManagementForm.name')}</div>
            <input
                className="bg-bg-primary border p-2 rounded-md w-[300px] mb-3"
                type="text"
                {...register('name', { required: true })}
                placeholder={t('consoleManagementForm.name') ?? ''}
            />
            {errors.name?.type === 'required' && (
                <p className="mb-3 text-fs-primary text-danger-500 h-3">
                    {t('consoleManagementForm.nameError')}
                </p>
            )}

            <div className="font-bold text-left">{t('consoleManagementForm.description')}</div>
            <input
                className="bg-bg-primary border p-2 rounded-md w-[300px] mb-3"
                type="text"
                {...register('description', { required: true })}
                placeholder={t('consoleManagementForm.description') ?? ''}
            />
            {errors.description?.type === 'required' && (
                <p className="mb-3 text-fs-primary text-danger-500 h-3">
                    {t('consoleManagementForm.descriptionError')}
                </p>
            )}

            <div className="font-bold text-left">{t('consoleManagementForm.dailyPrice')}</div>
            <input
                className="bg-bg-primary border p-2 rounded-md w-[300px] mb-3"
                type="number"
                {...register('dailyPrice', { required: true })}
                placeholder={t('consoleManagementForm.dailyPrice') ?? ''}
            />
            {errors.dailyPrice?.type === 'required' && (
                <p className="mb-3 text-fs-primary text-danger-500 h-3">
                    {t('consoleManagementForm.dailyPrice')}
                </p>
            )}

            <div className="pt-5 text-fs-h2">
                <Button
                    text={t('consoleManagementForm.update') ?? ''}
                    onClick={handleSubmit(async () => {
                        setError('')
                        await updateConsole(
                            new ConsoleUpdate(
                                consoleGet!.id,
                                watch('name'),
                                watch('description'),
                                watch('dailyPrice'),
                                []
                            )
                        ).then(() => {
                            getConsole(parseInt(id ?? '-1')).then((response) => {
                                setConsole(response.data)
                                setValue('name', response.data.name ?? '')
                                setValue('description', response.data.description ?? '')
                                setValue('dailyPrice', response.data.dailyPrice ?? 0)
                            })
                        })
                    })}
                />
            </div>
        </form>
    )
}

export default ConsoleInfoUpdateForm
