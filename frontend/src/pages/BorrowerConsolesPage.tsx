import { t } from 'i18next'
import { Link } from 'react-router-dom'
import { useEffect, useState } from 'react'
import { imagePathToURL } from '../models/Image'
import { useAuth } from '../contexts/AuthContext'
import ReactPaginate from 'react-paginate'
import { BorrowingGet, BorrowingStatus } from '../models/Borrowing'
import { getAllBorrowings } from '../api/BorrowingsApi'
import { getContainerHeight } from '../App'

function BorrowerConsolesPage() {
    const [borrowings, setBorrowings] = useState<BorrowingGet[]>()
    const { user } = useAuth()
    const itemsPerPage = 24
    const [loading, setLoading] = useState<boolean>(true)
    const [offset, setOffset] = useState<number>(0)
    const handlePageClick = (event: { selected: number }) => {
        const count = borrowings!
            .map((x) => x.userConsoles)
            .reduce((accumulator, currentValue) => accumulator.concat(currentValue)).length
        setOffset((event.selected * itemsPerPage) % count)
    }
    const [status, setStatus] = useState<BorrowingStatus>(BorrowingStatus.ACTIVE)

    useEffect(() => {
        getAllBorrowings().then((response) => {
            setBorrowings(response.data)
            setLoading(false)
        })
    }, [status])

    return (
        <div className="flex flex-col" style={{ minHeight: getContainerHeight() }}>
            {user?.role === 'admin' && (
                <div
                    className="mt-3 mx-auto w-max rounded-md shadow-[0_4px_9px_-4px_#3b71ca] transition duration-150 ease-in-out hover:bg-primary-600 hover:shadow-[0_8px_9px_-4px_rgba(59,113,202,0.3),0_4px_18px_0_rgba(59,113,202,0.2)] focus:bg-primary-600 focus:shadow-[0_8px_9px_-4px_rgba(59,113,202,0.3),0_4px_18px_0_rgba(59,113,202,0.2)] focus:outline-none focus:ring-0 active:bg-primary-700 active:shadow-[0_8px_9px_-4px_rgba(59,113,202,0.3),0_4px_18px_0_rgba(59,113,202,0.2)] dark:shadow-[0_4px_9px_-4px_rgba(59,113,202,0.5)] dark:hover:shadow-[0_8px_9px_-4px_rgba(59,113,202,0.2),0_4px_18px_0_rgba(59,113,202,0.1)] dark:focus:shadow-[0_8px_9px_-4px_rgba(59,113,202,0.2),0_4px_18px_0_rgba(59,113,202,0.1)] dark:active:shadow-[0_8px_9px_-4px_rgba(59,113,202,0.2),0_4px_18px_0_rgba(59,113,202,0.1)]"
                    role="group">
                    <button
                        type="button"
                        className={
                            status === BorrowingStatus.PENDING
                                ? 'inline-block rounded-l bg-primary-700 px-6 pb-2 pt-2.5 text-xs font-medium uppercase leading-normal text-white transition duration-150 ease-in-out hover:bg-primary-600 focus:bg-primary-600 focus:outline-none focus:ring-0 active:bg-primary-700'
                                : 'inline-block rounded-l bg-primary px-6 pb-2 pt-2.5 text-xs font-medium uppercase leading-normal text-white transition duration-150 ease-in-out hover:bg-primary-600 focus:bg-primary-600 focus:outline-none focus:ring-0 active:bg-primary-700'
                        }
                        onClick={() => setStatus(BorrowingStatus.PENDING)}>
                        {t('userConsolePage.statusUnconfirmed')}
                    </button>
                    <button
                        type="button"
                        className={
                            status === BorrowingStatus.ACTIVE
                                ? 'inline-block bg-primary-700 px-6 pb-2 pt-2.5 text-xs font-medium uppercase leading-normal text-white transition duration-150 ease-in-out hover:bg-primary-600 focus:bg-primary-600 focus:outline-none focus:ring-0 active:bg-primary-700'
                                : 'inline-block bg-primary px-6 pb-2 pt-2.5 text-xs font-medium uppercase leading-normal text-white transition duration-150 ease-in-out hover:bg-primary-600 focus:bg-primary-600 focus:outline-none focus:ring-0 active:bg-primary-700'
                        }
                        onClick={() => setStatus(BorrowingStatus.ACTIVE)}>
                        {t('userConsolePage.statusAtPlatform')}
                    </button>
                    <button
                        type="button"
                        className={
                            status === BorrowingStatus.AWAITING_TERMINATION
                                ? 'inline-block rounded-r bg-primary-700 px-6 pb-2 pt-2.5 text-xs font-medium uppercase leading-normal text-white transition duration-150 ease-in-out hover:bg-primary-600 focus:bg-primary-600 focus:outline-none focus:ring-0 active:bg-primary-700'
                                : 'inline-block rounded-r bg-primary px-6 pb-2 pt-2.5 text-xs font-medium uppercase leading-normal text-white transition duration-150 ease-in-out hover:bg-primary-600 focus:bg-primary-600 focus:outline-none focus:ring-0 active:bg-primary-700'
                        }
                        onClick={() => setStatus(BorrowingStatus.AWAITING_TERMINATION)}>
                        {t('userConsolePage.statusTerminating')}
                    </button>
                </div>
            )}
            <div className="grid sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:grid-cols-6 gap-5 m-3">
                {borrowings !== undefined &&
                    borrowings.length !== 0 &&
                    borrowings
                        .map((x) => x.userConsoles)
                        .reduce((accumulator, currentValue) => accumulator.concat(currentValue))
                        .slice(offset, offset + itemsPerPage)
                        .map((userConsole) => (
                            <Link
                                key={userConsole.id}
                                className="rounded-lg w-[250px] m-3 cursor-pointer select-none"
                                to={`/borrowings/${userConsole.id}`}>
                                <div className="relative">
                                    <div className="absolute right-1 bottom-1 bg-bg-primary rounded-md px-1">
                                        x {userConsole.amount}
                                    </div>
                                    {userConsole.images.length > 0 && (
                                        <img
                                            className="rounded-md"
                                            src={imagePathToURL(userConsole.images[0].path, 250)}
                                            alt={userConsole.images[0].name}
                                        />
                                    )}
                                </div>
                                <div className="text-t-secondary text-center">
                                    {userConsole.console.name}
                                </div>
                            </Link>
                        ))}
                <Link
                    key={-1}
                    className="fixed bottom-5 right-5 cursor-pointer select-none"
                    to={`/borrowings/new`}>
                    <div className="">
                        <span className="material-symbols-outlined text-[100px] w-full text-center">
                            add_circle
                        </span>
                    </div>
                </Link>
            </div>
            <div className="flex-1" />
            {!loading && (
                <ReactPaginate
                    className="ml-5 flex flex-row my-5 list-style-none"
                    previousLabel="Previous"
                    nextLabel="Next"
                    activeClassName="!bg-bg-extra"
                    pageClassName="relative block rounded bg-primary-100 px-3 py-1.5 text-sm font-medium text-primary-700 transition-all duration-300 mx-1"
                    previousLinkClassName="relative block rounded bg-primary-100 px-3 py-1.5 text-sm font-medium text-primary-700 transition-all duration-300 mx-1"
                    nextLinkClassName="relative block rounded bg-primary-100 px-3 py-1.5 text-sm font-medium text-primary-700 transition-all duration-300 mx-1"
                    breakLabel="..."
                    onPageChange={(e) => handlePageClick(e)}
                    pageRangeDisplayed={5}
                    pageCount={Math.ceil(borrowings!.length / itemsPerPage)}
                    renderOnZeroPageCount={null}
                />
            )}
        </div>
    )
}

export default BorrowerConsolesPage
