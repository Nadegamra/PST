import { SubmitHandler, useForm } from 'react-hook-form'
import { sendPasswordResetEmail } from '../api/UsersApi'
import { useState } from 'react'
import { t } from 'i18next'

interface Props {
    email: string
}

function ForgotPasswordPage() {
    const [error, setError] = useState('')
    const [message, setMessage] = useState('')
    const [loading, setLoading] = useState<boolean>(false)

    const {
        register,
        handleSubmit,
        watch,
        formState: { errors }
    } = useForm<Props>()

    const onSubmit: SubmitHandler<Props> = () => {
        setLoading(true)
        setError('')
        sendPasswordResetEmail(watch('email'))
            .then(() => {
                setLoading(false)
                setMessage('Please check your email for password reset code')
            })
            .catch((error) => setError(error))
    }

    return (
        <form
            className="flex flex-col items-center select-none bg-bg-primary text-t-primary pt-10"
            onSubmit={handleSubmit(onSubmit)}>
            <div className="w-80 bg-bg-secondary pb-5 rounded">
                <div className="py-6 text-fs-h1 text-center">
                    {t('passwordReset.passwordReset')}
                </div>
                <div className="mx-[30px]">
                    <input
                        type="email"
                        className="w-full bg-bg-secondary border-b focus:outline-none"
                        placeholder={t('passwordReset.email') ?? ''}
                        {...register('email', { required: true })}
                        disabled={loading}
                    />
                    <p className="mb-3 text-fs-primary text-danger-500 h-3">
                        {errors.email?.type === 'required' ? t('passwordReset.emailError') : ''}
                    </p>
                </div>
                <div className="flex flex-col items-center pt-3">
                    <button
                        type="submit"
                        className="bg-bg-extra py-1 px-7 rounded"
                        disabled={loading}>
                        {t('passwordReset.sendEmail')}
                    </button>
                </div>
            </div>
            <div className="pt-4 text-fs-primary text-danger-500 text-center">{error}</div>
            {error === '' && (
                <div className="pt-4 text-fs-primary text-success-500 text-center">{message}</div>
            )}
            {loading && (
                <div className="flex items-center justify-center pt-10">
                    <div className="w-16 h-16 border-b-2 border-gray-900 rounded-full animate-spin"></div>
                </div>
            )}
        </form>
    )
}

export default ForgotPasswordPage
