export function getAuthHeader(email:string, password:string){
    const encoded = btoa(`${email}:${password}`)
    return `Basic ${encoded}` ;
}