import { ConsoleGet } from './Console'
import { ImageAdd, ImageGet, ImageUpdate } from './Image'
import { UserGet } from './User'

export class UserConsoleGet {
    id: number
    userId: number
    user: UserGet
    consoleId: number
    console: ConsoleGet
    borrowingId: number
    conversationId: number
    amount: number
    accessories: string
    images: ImageGet[]
    consoleStatus: UserConsoleStatus

    constructor(
        id: number,
        userId: number,
        user: UserGet,
        consoleId: number,
        console: ConsoleGet,
        borrowingId: number,
        conversationId: number,
        amount: number,
        accessories: string,
        images: ImageGet[],
        consoleStatus: UserConsoleStatus
    ) {
        this.id = id
        this.userId = userId
        this.user = user
        this.consoleId = consoleId
        this.console = console
        this.borrowingId = borrowingId
        this.conversationId = conversationId
        this.amount = amount
        this.accessories = accessories
        this.images = images
        this.consoleStatus = consoleStatus
    }
}

export class UserConsoleAdd {
    consoleId: number
    amount: number
    accessories: string
    images: ImageAdd[]

    constructor(consoleId: number, amount: number, accessories: string, images: ImageAdd[]) {
        this.consoleId = consoleId
        this.amount = amount
        this.accessories = accessories
        this.images = images
    }
}

export class UserConsoleUpdate {
    id: number
    consoleId: number
    amount: number
    accessories: string
    images: ImageUpdate

    constructor(
        id: number,
        consoleId: number,
        amount: number,
        accessories: string,
        images: ImageUpdate
    ) {
        this.id = id
        this.consoleId = consoleId
        this.amount = amount
        this.accessories = accessories
        this.images = images
    }
}

export class UserConsoleStatusUpdate {
    id: number
    consoleStatus: UserConsoleStatus

    constructor(id: number, consoleStatus: UserConsoleStatus) {
        this.id = id
        this.consoleStatus = consoleStatus
    }
}

export class UserConsolesStatusRequest {
    consoleStatus: UserConsoleStatus

    constructor(consoleStatus: UserConsoleStatus) {
        this.consoleStatus = consoleStatus
    }
}

export enum UserConsoleStatus {
    UNCONFIRMED,
    AT_PLATFORM,
    RESERVED,
    AT_LENDER,
    AWAITING_TERMINATION_BY_LENDER,
    AWAITING_TERMINATION_BY_BORROWER
}

export function getConsoleStatusString(status: UserConsoleStatus) {
    if (status === UserConsoleStatus.UNCONFIRMED) {
        return 'userConsolePage.statusUnconfirmed'
    } else if (status === UserConsoleStatus.AT_PLATFORM || status === UserConsoleStatus.RESERVED) {
        return 'userConsolePage.statusAtPlatform'
    }
    // else if (status === UserConsoleStatus.RESERVED) {
    //     return 'userConsolePage.statusReserved'
    // }
    else if (status === UserConsoleStatus.AT_LENDER) {
        return 'userConsolePage.statusAtLender'
    } else {
        return 'userConsolePage.statusTerminating'
    }
}

export function getConsoleStatusStringBorrower(status: UserConsoleStatus) {
    if (status === UserConsoleStatus.RESERVED) {
        return 'borrowing.statusPending'
    } else if (status === UserConsoleStatus.AT_LENDER) {
        return 'borrowing.statusActive'
    } else {
        return 'borrowing.statusTerminating'
    }
}
