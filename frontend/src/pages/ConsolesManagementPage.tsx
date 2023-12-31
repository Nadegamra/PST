import { useEffect, useState } from 'react'
import { ConsoleGet } from '../models/Console'
import { getConsoles } from '../api/ConsolesApi'
import { imagePathToURL } from '../models/Image'
import { Link } from 'react-router-dom'
import ReactPaginate from 'react-paginate'
import { getContainerHeight } from '../App'

function ConsolesManagementPage() {
    const [consoles, setConsoles] = useState<ConsoleGet[]>()
    const itemsPerPage = 24
    const [loading, setLoading] = useState<boolean>(true)
    const [offset, setOffset] = useState<number>(0)
    const handlePageClick = (event: { selected: number }) => {
        setOffset((event.selected * itemsPerPage) % consoles!.length)
    }

    useEffect(() => {
        getConsoles()
            .then((response) => {
                setConsoles(response.data)
            })
            .finally(() => setLoading(false))
    }, [])

    return (
        <div className="flex flex-col" style={{ minHeight: getContainerHeight() }}>
            <div className="flex-1 grid sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:grid-cols-6 gap-5 m-3">
                {consoles?.slice(offset, offset + itemsPerPage).map((console) => (
                    <Link
                        key={console.id}
                        className="rounded-lg cursor-pointer select-none w-max h-max mx-auto mt-5"
                        to={`/manageConsoles/${console.id}`}>
                        {console.images.length > 0 && (
                            <img
                                className="rounded-md"
                                src={imagePathToURL(console.images[0].path, 250)}
                                alt={console.images[0].name}
                            />
                        )}

                        <div className="text-t-secondary text-center">{console.name}</div>
                    </Link>
                ))}
                <Link
                    key={-1}
                    className="fixed bottom-5 right-5 cursor-pointer select-none"
                    to={`/manageConsoles/new`}>
                    <div className="">
                        <span className="material-symbols-outlined text-[100px] w-full text-center">
                            add_circle
                        </span>
                    </div>
                </Link>
            </div>
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
                    pageCount={Math.ceil(consoles!.length / itemsPerPage)}
                    renderOnZeroPageCount={null}
                />
            )}
        </div>
    )
}

export default ConsolesManagementPage
