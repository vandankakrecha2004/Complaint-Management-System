import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Complaint {
  private apiUrl = 'https://localhost:7178/api/Complaint'; // <-- your API URL

  constructor(private http: HttpClient) {}

  getAllComplaints() {
    return this.http.get<any[]>(this.apiUrl);
  }

  addComplaint(data: any) {
    return this.http.post(`${this.apiUrl}`, data);
  }

  changeComplaintStatus(data: any) {
    return this.http.put(`${this.apiUrl}/status`, data);
  }

  getSubjects(systemTypeId?: number) {
    return this.http.get<any[]>(`${this.apiUrl}/subjects`, {
      params: systemTypeId ? { systemTypeId: systemTypeId } : {},
    });
  }

  getSystemTypes() {
    return this.http.get<any[]>(`${this.apiUrl}/system-types`);
  }

  uploadComplaintFile(formData: FormData) {
    return this.http.post(`${this.apiUrl}/upload-complaint-file`, formData);
  }

  allocateComplaint(data: any) {
    return this.http.post(`${this.apiUrl}/allocate`, data);
  }

  assignComplaint(data: any) {
    return this.http.post(`${this.apiUrl}/assign`, data);
  }

  downloadComplaintFile(id: number, type: string) {
    return this.http.get(`${this.apiUrl}/download-file/${id}?type=${type}`, {
      responseType: 'blob',
    });
  }

  getStatusHistory(id: number) {
    return this.http.get<any[]>(`${this.apiUrl}/status-history/${id}`);
  }
}
