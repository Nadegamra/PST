import { ReactNode, useEffect, useReducer, useState } from 'react'
import { useAuth } from '../../contexts/AuthContext'
import { useHeader } from '../../contexts/HeaderContext'
import { useTranslation } from 'react-i18next'
import ReactCountryFlag from 'react-country-flag'
import { useMediaQuery } from 'react-responsive'
import { useLocation, useNavigate } from 'react-router'
import { Link } from 'react-router-dom'
function Header() {
    const { t, i18n } = useTranslation()
    const [darkmode, setDarkmode] = useState<boolean>(
        (localStorage.getItem('data-theme') ?? 'dark') == 'dark'
    )
    const auth = useAuth()
    const location = useLocation()
    const navigate = useNavigate()
    const toggleDarkMode = (darkmode: boolean) => {
        setDarkmode(darkmode)
        localStorage.setItem('data-theme', darkmode ? 'dark' : 'light')
        document.documentElement.setAttribute('data-theme', darkmode ? 'dark' : 'light')
    }

    const ButtonText = ({ children }: { children: ReactNode }) => {
        return <b className="hover:text-t-hover text-fs-h2">{children}</b>
    }
    const [ignored, forceUpdate] = useReducer((x) => x + 1, 0)
    const header = useHeader()

    useEffect(() => {
        document.documentElement.setAttribute(
            'data-theme',
            localStorage.getItem('data-theme') ?? 'dark'
        )
    }, [])

    return (
        <nav
            className="flex-no-wrap relative flex w-full items-center justify-between bg-bg-secondary py-2 shadow-md shadow-black/5 dark:shadow-black/10 lg:flex-wrap lg:justify-start "
            data-te-navbar-ref
            key={ignored}>
            <div className="flex w-full flex-wrap items-center justify-between px-3">
                <button
                    className="block border-0 bg-transparent px-2 text-neutral-500 hover:no-underline hover:shadow-none focus:no-underline focus:shadow-none focus:outline-none focus:ring-0 dark:text-neutral-200 lg:hidden"
                    type="button"
                    data-te-collapse-init
                    data-te-target="#navbarSupportedContent1"
                    aria-controls="navbarSupportedContent1"
                    aria-expanded="false"
                    aria-label="Toggle navigation">
                    <span className="[&>svg]:w-7 mx-auto">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 24 24"
                            fill="currentColor"
                            className="h-7 w-7">
                            <path
                                fillRule="evenodd"
                                d="M3 6.75A.75.75 0 013.75 6h16.5a.75.75 0 010 1.5H3.75A.75.75 0 013 6.75zM3 12a.75.75 0 01.75-.75h16.5a.75.75 0 010 1.5H3.75A.75.75 0 013 12zm0 5.25a.75.75 0 01.75-.75h16.5a.75.75 0 010 1.5H3.75a.75.75 0 01-.75-.75z"
                                clipRule="evenodd"
                            />
                        </svg>
                    </span>
                </button>
                <div
                    className="!visible hidden flex-grow basis-[100%] items-center lg:!flex lg:basis-auto"
                    id="navbarSupportedContent1"
                    data-te-collapse-item>
                    <Link
                        className="mb-4 mr-2 mt-3 flex items-center lg:mr-0 lg:mb-0 lg:mt-0"
                        to="/"
                        onClick={() => {
                            header.hideAll()
                            forceUpdate()
                        }}>
                        <img
                            className="h-[30px]"
                            src={darkmode ? '/logoLight.png' : '/logoDark.png'}
                            alt=""
                            loading="lazy"
                        />
                    </Link>

                    <ul
                        className="list-style-none ml-auto flex flex-col pl-3 lg:pl-0 lg:flex-row "
                        data-te-navbar-nav-ref>
                        {auth.user !== undefined && auth.user.role === 'lender' && (
                            <Link
                                className="cursor-pointer select-none mb-2 mr-5 pt-2"
                                to="/consoles"
                                onClick={() => {
                                    header.hideAll()
                                    forceUpdate()
                                }}>
                                {t('header.myConsoles')}
                            </Link>
                        )}
                        {auth.user !== undefined && auth.user.role === 'borrower' && (
                            <Link
                                className="cursor-pointer select-none mb-2 mr-5 pt-2"
                                to="/borrowings"
                                onClick={() => {
                                    header.hideAll()
                                    forceUpdate()
                                }}
                                children={t('header.myConsoles')}
                            />
                        )}
                        {auth.user !== undefined && auth.user.role === 'admin' && (
                            <Link
                                className="cursor-pointer select-none mb-2 mr-5 pt-2"
                                to="/approveRegistrations"
                                onClick={() => {
                                    header.hideAll()
                                    forceUpdate()
                                }}
                                children={t('header.approveRegistrations')}
                            />
                        )}
                        {auth.user !== undefined && auth.user.role === 'admin' && (
                            <Link
                                className="cursor-pointer select-none mb-2 mr-5 pt-2"
                                to="/manageConsoles"
                                onClick={() => {
                                    header.hideAll()
                                    forceUpdate()
                                }}
                                children={t('header.manageConsoles')}
                            />
                        )}
                        {auth.user !== undefined && auth.user.role === 'admin' && (
                            <Link
                                className="cursor-pointer select-none mb-2 mr-5 pt-2"
                                to="/userConsoles"
                                onClick={() => {
                                    header.hideAll()
                                    forceUpdate()
                                }}
                                children={t('header.lendRequests')}
                            />
                        )}
                        {auth.user !== undefined && auth.user.role === 'admin' && (
                            <Link
                                className="cursor-pointer select-none mb-2 mr-5 pt-2"
                                to="/manageBorrowings"
                                onClick={() => {
                                    header.hideAll()
                                    forceUpdate()
                                }}
                                children={t('header.borrowRequests')}
                            />
                        )}
                        <Link
                            className="cursor-pointer select-none mb-2 mr-5 pt-2"
                            to="/contacts"
                            onClick={() => {
                                header.hideAll()
                                forceUpdate()
                            }}
                            children={t('header.contacts')}
                        />
                        <Link
                            className="cursor-pointer select-none mb-2 mr-5 pt-2"
                            to="/faq"
                            onClick={() => {
                                header.hideAll()
                                forceUpdate()
                            }}
                            children={t('header.FAQ')}
                        />
                        {auth.user === undefined && (
                            <Link
                                className="cursor-pointer select-none mb-2 mr-5 pt-2"
                                to="/login"
                                onClick={() => {
                                    header.hideAll()
                                    forceUpdate()
                                }}
                                children={t('header.login')}
                            />
                        )}
                        {auth.user === undefined && (
                            <Link
                                className="cursor-pointer select-none mb-2 mr-5 pt-2"
                                to="/register"
                                onClick={() => {
                                    header.hideAll()
                                    forceUpdate()
                                }}
                                children={t('header.register')}
                            />
                        )}
                    </ul>
                </div>

                {/* <!-- Right elements --> */}
                <div className="relative flex items-center pl-3 lg:pl-0">
                    <button
                        className="pr-3 select-none"
                        value="en"
                        onClick={() => i18n.changeLanguage('en')}>
                        <ReactCountryFlag countryCode="GB" svg />
                    </button>
                    <button
                        className="pr-5 select-none"
                        value="lt"
                        onClick={() => i18n.changeLanguage('lt')}>
                        <ReactCountryFlag countryCode="LT" svg />
                    </button>
                    {auth.user !== undefined && (
                        <Link
                            className="cursor-pointer select-none my-auto mr-5 pt-2"
                            to="/chats"
                            onClick={() => {
                                header.hideAll()
                                forceUpdate()
                                navigate('/chats')
                            }}>
                            <ButtonText>
                                <span className="material-symbols-outlined">chat</span>
                            </ButtonText>
                        </Link>
                    )}
                    <div
                        className="my-auto"
                        onClick={() => {
                            toggleDarkMode(!darkmode)
                        }}>
                        <span className="material-symbols-outlined align-middle mr-5 cursor-pointer select-none">
                            {darkmode ?? false ? 'light_mode' : 'dark_mode'}
                        </span>
                    </div>
                    {auth.user !== undefined && (
                        <div className="mr-3">
                            <button>
                                <span
                                    className="material-symbols-outlined cursor-pointer select-none pt-2"
                                    onClick={() =>
                                        header.profileShown
                                            ? header.hideAll()
                                            : header.showProfile()
                                    }>
                                    manage_accounts
                                </span>
                            </button>
                            {header.profileShown && (
                                <div className="fixed mt-5 translate-x-[-110px] w-[130px] bg-bg-secondary p-3 rounded-lg">
                                    <Link
                                        onClick={() => {
                                            header.hideAll()
                                            forceUpdate()
                                        }}
                                        to="/profile"
                                        className="pb-1">
                                        <span className="material-symbols-outlined align-middle pr-3 cursor-pointer select-none">
                                            person
                                        </span>
                                        <span className="cursor-pointer select-none">
                                            {t('header.profile.profile')}
                                        </span>
                                    </Link>
                                    <button
                                        className="pt-1"
                                        onClick={() => {
                                            auth.logout()
                                            header.hideAll()
                                            forceUpdate()
                                        }}>
                                        <span className="material-symbols-outlined pr-3 align-middle">
                                            logout
                                        </span>
                                        <span className="cursor-pointer select-none">
                                            {t('header.profile.logout')}
                                        </span>
                                    </button>
                                </div>
                            )}
                        </div>
                    )}
                </div>
            </div>
        </nav>
    )
}

export default Header
