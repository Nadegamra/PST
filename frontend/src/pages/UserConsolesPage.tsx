import { UserConsoleStatus } from '../models/UserConsole'
import { useState } from 'react'
import { useAuth } from '../contexts/AuthContext'
import UserConsoleStatusSelectionAdmin from '../components/userConsoles/UserConsolesStatusSelectionAdmin'
import UserConsolesGrid from '../components/userConsoles/UserConsolesGrid'
import BorrowingsList from '../components/userConsoles/BorrowingsList'

function UserConsolesPage() {
    const [status, setStatus] = useState<UserConsoleStatus>(UserConsoleStatus.UNCONFIRMED)

    const { user } = useAuth()

    return (
        <div
            className="flex flex-col"
            style={{ height: document.getElementById('container')?.clientHeight }}>
            <div id="userConsolesContainer" className="flex-1">
                {user?.role === 'admin' && (
                    <UserConsoleStatusSelectionAdmin status={status} setStatus={setStatus} />
                )}
                {status <= UserConsoleStatus.AT_PLATFORM ? (
                    <UserConsolesGrid status={status} />
                ) : (
                    <BorrowingsList status={status} />
                )}
            </div>
        </div>
    )
}

export default UserConsolesPage
