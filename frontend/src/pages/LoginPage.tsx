import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { useAuth } from '../contexts/AuthContext'
import { UserLogin } from '../models/User'
import { useTranslation } from 'react-i18next'
import { Link } from 'react-router-dom'
import Button from '../components/ui/Button'

function LoginPage() {
    const { t } = useTranslation()
    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm<UserLogin>()
    const [error, setError] = useState('')
    const { login, loading } = useAuth()

    return (
        <form
            className="flex flex-col items-center select-none bg-bg-primary text-t-primary pt-10"
            onSubmit={handleSubmit((data) => {
                setError('')
                login(data).then((error) => setError(error))
            })}>
            <div className="w-80 bg-bg-secondary pb-5 rounded">
                <div className="py-6 text-fs-h1 text-center">{t('login.login')}</div>
                <div className="mx-[30px]">
                    <input
                        type="text"
                        className="w-full bg-bg-primary border p-2 rounded-md text-fs-h2"
                        placeholder={t('login.username') ?? ''}
                        {...register('username', { required: true })}
                        disabled={loading}
                    />
                    <p className="mb-3 text-fs-primary text-danger-500 h-3">
                        {errors.username?.type === 'required' ? 'Username is required' : ''}
                    </p>

                    <input
                        type="password"
                        className="w-full bg-bg-primary border p-2 rounded-md"
                        placeholder={t('login.password') ?? ''}
                        {...register('password', { required: true })}
                        disabled={loading}
                    />
                    <p className="mb-3 text-fs-primary text-danger-500 h-3">
                        {errors.password?.type === 'required' ? 'Password is required' : ''}
                    </p>
                </div>
                <div className="flex flex-row content-around w-full ml-[30px] mb-3">
                    <label
                        htmlFor="remember"
                        className="text-left pb-[2px] cursor-pointer text-fs-primary">
                        <input
                            id="remember"
                            type="checkbox"
                            className="form-checkbox mr-[10px] w-4 h-4 rounded hover:bg-bg-extra checked:bg-bg-extra bg-bg-secondary focus:ring-0 focus:outline-none border-t-primary border-t-t-primary"
                            {...register('rememberPassword')}
                            disabled={loading}
                        />
                        {t('login.rememberMe')}
                    </label>
                    <Link
                        to="/forgotPassword"
                        className="cursor-pointer select-none my-auto ml-auto text-fs-primary mr-12">
                        {t('login.forgotPassword')}
                    </Link>
                </div>
                <div className="flex flex-col items-center pt-5 text-fs-h2">
                    <Button text={t('login.login')} submit={true} />
                </div>
            </div>
            <div className="pt-4 text-fs-primary text-danger-500 text-center">{error}</div>
            {loading && (
                <div className="flex items-center justify-center pt-10">
                    <div className="w-16 h-16 border-b-2 border-gray-900 rounded-full animate-spin"></div>
                </div>
            )}
        </form>
    )
}

export default LoginPage
