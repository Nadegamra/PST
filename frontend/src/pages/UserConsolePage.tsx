import { useParams } from 'react-router'
import {
    UserConsoleStatus,
    UserConsoleGet,
    UserConsoleStatusUpdate,
    getConsoleStatusString
} from '../models/UserConsole'
import { useEffect, useState } from 'react'
import { getUserConsole, terminateContract, updateUserConsoleStatus } from '../api/UserConsolesApi'
import 'react-responsive-carousel/lib/styles/carousel.min.css'
import { Carousel } from 'react-responsive-carousel'
import { imagePathToURL } from '../models/Image'
import Button from '../components/ui/Button'
import { useAuth } from '../contexts/AuthContext'
import { useTranslation } from 'react-i18next'
import { contactLender } from '../api/ChatsApi'
import { Controller, useForm } from 'react-hook-form'
import Select from 'react-select'
import { BorrowingGet, BorrowingStatus, getBorrowingStatusString } from '../models/Borrowing'
import { getBorrowingById } from '../api/BorrowingsApi'
import { response } from 'express'

interface Props {
    status: UserConsoleStatus
}

function UserConsolePage() {
    const { id } = useParams()
    const { t } = useTranslation()
    const {
        register,
        watch,
        handleSubmit,
        formState: { errors },
        control,
        setValue
    } = useForm<Props>()
    const options = [
        { value: UserConsoleStatus.UNCONFIRMED, label: t('userConsolePage.statusUnconfirmed') },
        { value: UserConsoleStatus.AT_PLATFORM, label: t('userConsolePage.statusAtPlatform') },
        { value: UserConsoleStatus.AT_LENDER, label: t('userConsolePage.statusAtLender') },
        {
            value: UserConsoleStatus.AWAITING_TERMINATION,
            label: t('userConsolePage.statusTerminating')
        }
    ]
    const [borrowing, setBorrowing] = useState<BorrowingGet>()
    const [userConsole, setUserConsole] = useState<UserConsoleGet>()
    const { user } = useAuth()
    const [loading, setLoading] = useState<boolean>(true)
    useEffect(() => {
        getUserConsole(parseInt(id ?? '1')).then((result) => {
            setUserConsole(result.data)
            if (user?.role === 'borrower') {
                getBorrowingById((result.data as UserConsoleGet).borrowingId).then((response) => {
                    setBorrowing(response.data)
                    setLoading(false)
                })
            } else {
                setLoading(false)
            }
        })
    }, [])

    return (
        <div className="mt-10 mx-auto flex flex-col md:flex-row">
            <div className="md:h-[400px] md:w-[700px] text-center align-middle mx-10">
                {userConsole !== undefined && (
                    <Carousel>
                        {userConsole?.images.map((value) => (
                            <div>
                                <p className="legend">{value.name}</p>
                                <img src={imagePathToURL(value.path, 1080)} alt={value.name} />
                            </div>
                        ))}
                    </Carousel>
                )}
            </div>
            <div className="ml-5 md:ml-0 mb-10 min-w-[400px]">
                <div className="text-fs-h1">{t('userConsolePage.consoleTitle')}</div>
                <hr className="pb-2" />
                <div className="font-bold">{t('userConsolePage.consoleName')}</div>
                <div className="ml-3">{userConsole?.console.name}</div>
                <div className="font-bold">{t('userConsolePage.consoleDescription')}</div>
                <div className="ml-3">{userConsole?.console.description}</div>
                <div className="font-bold">{t('userConsolePage.dailyPrice')}</div>
                <div className="ml-3">
                    {user?.role !== 'borrower'
                        ? (Math.round((userConsole?.console.dailyPrice ?? 0) * 0.6 * 100) / 100) *
                          30
                        : userConsole?.console.dailyPrice}{' '}
                    €
                </div>

                <div className="text-fs-h1 mt-5">{t('userConsolePage.lendTitle')}</div>
                <hr className="pb-2" />
                <div className="font-bold">{t('userConsolePage.lendAmount')}</div>
                <div className="ml-3">{userConsole?.amount}</div>
                <div className="font-bold">{t('userConsolePage.lendAccessories')}</div>
                <div className="ml-3">{userConsole?.accessories}</div>
                <div className="font-bold">{t('userConsolePage.lendStatus')}</div>
                {user?.role === 'borrower' && !loading && (
                    <div className="ml-3">{t(getBorrowingStatusString(borrowing!.status))}</div>
                )}
                {user?.role === 'lender' && !loading && (
                    <div className="ml-3">
                        {t(
                            getConsoleStatusString(
                                userConsole?.consoleStatus ?? UserConsoleStatus.UNCONFIRMED
                            )
                        )}
                    </div>
                )}
                {user?.role === 'admin' && !loading && (
                    <div>
                        <Controller
                            control={control}
                            name="status"
                            rules={{ required: true }}
                            render={() => (
                                <Select
                                    className="!bg-bg-secondary !text-[rgb(0,0,0)] mb-5"
                                    defaultValue={
                                        options.filter(
                                            (x) => x.value === userConsole?.consoleStatus
                                        )[0]
                                    }
                                    options={options}
                                    onChange={(e) => {
                                        setValue(
                                            'status',
                                            e?.value ?? UserConsoleStatus.UNCONFIRMED
                                        )
                                    }}
                                />
                            )}
                        />
                        <Button
                            text={t('userConsolePage.changeStatus')}
                            dialog={true}
                            dialogBody={t('button.dialogBody2')}
                            onClick={() => {
                                updateUserConsoleStatus(
                                    new UserConsoleStatusUpdate(userConsole!.id, watch('status'))
                                ).then(() => {
                                    window.location.href = '/userConsoles'
                                })
                            }}
                        />
                    </div>
                )}
                {user?.role === 'lender' &&
                    !loading &&
                    userConsole?.consoleStatus !== UserConsoleStatus.UNCONFIRMED &&
                    userConsole?.consoleStatus !== UserConsoleStatus.AWAITING_TERMINATION && (
                        <div className="mt-5">
                            <Button
                                text={t('userConsolePage.initiateTermination')}
                                dialog={true}
                                dialogBody={t('button.dialogBody1')}
                                onClick={() => {
                                    terminateContract(userConsole?.id ?? -1)
                                }}
                            />
                        </div>
                    )}
                {user?.role === 'borrower' &&
                    borrowing?.status === BorrowingStatus.ACTIVE &&
                    !loading &&
                    userConsole?.consoleStatus !== UserConsoleStatus.UNCONFIRMED &&
                    userConsole?.consoleStatus !== UserConsoleStatus.AWAITING_TERMINATION && (
                        <div className="mt-5">
                            <Button
                                text={t('userConsolePage.initiateTermination')}
                                dialog={true}
                                dialogBody={t('button.dialogBody1')}
                                onClick={() => {
                                    terminateContract(userConsole?.id ?? -1)
                                }}
                            />
                        </div>
                    )}
                {user?.role === 'admin' && (
                    <div>
                        <div className="text-fs-h1 mt-5">{t('userConsolePage.userTitle')}</div>
                        <hr className="pb-2" />
                        <div className="font-bold">{t('userConsolePage.userFname')}</div>
                        <div className="ml-3">{userConsole?.user.firstName}</div>
                        <div className="font-bold">{t('userConsolePage.userLname')}</div>
                        <div className="ml-3">{userConsole?.user.lastName}</div>
                        <div className="font-bold">{t('userConsolePage.userEmail')}</div>
                        <div className="ml-3 mb-3">{userConsole?.user.email}</div>
                        <Button
                            text={t('userConsolePage.contactUser')}
                            dialog={false}
                            onClick={() => {
                                contactLender(userConsole!.id).then(() => {
                                    window.location.href = `/chats/${userConsole?.conversationId}`
                                })
                            }}
                            dialogBody={t('button.dialogBody1')}
                        />
                    </div>
                )}
            </div>
        </div>
    )
}

export default UserConsolePage
