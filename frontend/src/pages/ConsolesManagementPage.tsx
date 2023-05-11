import { useEffect, useState } from 'react'
import { ConsoleGet } from '../models/Console'
import { getConsoles } from '../api/ConsolesApi'
import { imagePathToURL } from '../models/Image'
import { Link } from 'react-router-dom'
import { useTranslation } from 'react-i18next'

function ConsolesManagementPage() {
    const { t } = useTranslation()
    const [consoles, setConsoles] = useState<ConsoleGet[]>()

    useEffect(() => {
        getConsoles().then((response) => {
            setConsoles(response.data)
        })
    }, [])

    return (
        <div className="grid sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xl:grid-cols-5 2xl:grid-cols-6 gap-5 m-3">
            {consoles?.map((console) => (
                <Link
                    className="rounded-lg w-[250px] m-3 cursor-pointer select-none"
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
    )
}

export default ConsolesManagementPage
